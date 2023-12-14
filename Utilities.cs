
namespace Monopoly
{
    public static class Utilities
    {
        static Random r = new Random();

        public static void Shuffle<T>(this T[] deck)
        {
            for (int i = deck.Length - 1; i > 0; --i)
            {
                int card = r.Next(i + 1);
                T temp = deck[i];
                deck[i] = deck[card];
                deck[card] = temp;
            }
        }

        public static List<string> ReadCSV(string Dir)
        {
            var Path = AppDomain.CurrentDomain.BaseDirectory + Dir;
            var list = new List<string>();
            Console.Write(Path);
            using (var stream = new FileStream(Path, FileMode.Open))
            {
                string text;
                var reader = new StreamReader(stream);
                while ((text = reader.ReadLine()) != null)
                {
                    list.Add(text);
                }
            }

            list.RemoveAt(0);
            return list;
        }
    }
}
