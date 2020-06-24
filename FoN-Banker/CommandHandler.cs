using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace FoNBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;
        private readonly IServiceProvider serviceProvider;

        public CommandHandler(DiscordSocketClient client, CommandService commandService,
            IServiceProvider serviceProvider)
        {
            this.client = client;
            this.commandService = commandService;
            this.serviceProvider = serviceProvider;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += OnMessageReceivedAsync;

            await commandService.AddModulesAsync(Assembly.GetEntryAssembly(), serviceProvider);
        }

        private async Task OnMessageReceivedAsync(SocketMessage msg)
        {
            if (!(msg is SocketUserMessage message)) return;

            var argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) ||
                  message.HasMentionPrefix(client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(client, message);

            await commandService.ExecuteAsync(context, argPos, serviceProvider);
        }
    }
}