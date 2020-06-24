using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using FoNBot.Service;

namespace FoNBot.Module
{
    public class BankerModule : ModuleBase<SocketCommandContext>
    {
        private const int embeddedFieldMaxLength = 1024;
        private readonly ItemParserService itemParser;

        public BankerModule(ItemParserService itemParser)
        {
            this.itemParser = itemParser;
        }

        [Command("updatebuffbank")]
        public async Task UpdateBuffBankAsync([Remainder] string input)
        {
            var items = itemParser.ParseItems(input);

            var embed = new EmbedBuilder
            {
                Title = "Buff Bank Contents"
            };
            var fancyMessage = embed.WithColor(Color.Purple);

            var categoryBuilder = new StringBuilder();
            var count = 0;
            foreach (var bankItem in items)
            {
                var line = bankItem.Url != null
                    ? $"[{bankItem.Name}]({bankItem.Url}) - {bankItem.Count}"
                    : $"{bankItem.Name} - {bankItem.Count}";

                if (categoryBuilder.Length + line.Length > embeddedFieldMaxLength)
                {
                    fancyMessage.AddField($"Category Title {++count}", categoryBuilder.ToString(), true);
                    categoryBuilder.Clear();
                }

                categoryBuilder.AppendLine(line);
            }

            var categoryTitle = count == 0 ? "Category Title" : $"Category Title {++count}";
            fancyMessage.AddField(categoryTitle, categoryBuilder.ToString(), true);

            await Context.Channel.SendMessageAsync(null, false, fancyMessage.Build());
        }

        [Command("test")]
        public async Task TestAsync()
        {
            var embed = new EmbedBuilder
            {
                Title = "Crafting Materials"
            };

            var mcBuilder = new StringBuilder();
            mcBuilder
                .AppendLine("[Lava Core](https://classic.wowhead.com/item=17011/lava-core) - 23")
                .AppendLine("[Fiery Core](https://classic.wowhead.com/item=17010/fiery-core) - 40");

            var zgBuilder = new StringBuilder();
            zgBuilder
                .AppendLine("[Bloodvine](https://classic.wowhead.com/item=19726/bloodvine) - 5")
                .AppendLine("[Souldarite](https://classic.wowhead.com/item=19774/souldarite) - 2");

            var fancyMessage = embed
                .WithColor(Color.Purple)
                .AddField(_ => _
                    .WithName("Molten Core")
                    .WithValue(mcBuilder.ToString()))
                .AddField(_ => _
                    .WithName("Zul'Gurub")
                    .WithValue(zgBuilder.ToString()))
                .Build();
            await Context.Channel.SendMessageAsync(null, false, fancyMessage);
        }
    }
}