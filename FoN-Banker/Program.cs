using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace FoNBot
{
    internal class Program
    {
        private DiscordSocketClient client;
        private CommandHandler commandHandler;

        private static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;

            var initialization = new ApplicationInitialization();
            initialization.RegisterDependencies();

            commandHandler = new CommandHandler(client, new CommandService(),
                new CastleServiceProvider(initialization.Container));
            await commandHandler.InstallCommandsAsync();

            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("FoNBotToken"));
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            // TODO: Output to Castle Logging framework
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}