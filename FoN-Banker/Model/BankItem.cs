namespace FoNBot.Model
{
    public class BankItem
    {
        public BankItem(string name, int count, string url = null)
        {
            Name = name;
            Count = count;
            Url = url;
        }

        public string Name { get; }

        public int Count { get; }

        public string Url { get; }
    }
}