using System.Collections.Generic;
using System.Linq;
using FoNBot.Model;

namespace FoNBot.Service
{
    public class ItemParserService
    {
        public ICollection<BankItem> ParseItems(string inputString)
        {
            var bankItems = new List<BankItem>();
            string[] itemStrings = inputString.Split("\\n");

            foreach (string itemPair in itemStrings.Where(_ => !string.IsNullOrEmpty(_)))
            {
                var itemString = itemPair.Split(',');
                if (int.TryParse(itemString[1], out int count))
                {
                    bankItems.Add(new BankItem(itemString[0], count));
                }

                // TODO: Maybe some error logging if TryParse fails
            }

            return bankItems;
        }
    }
}