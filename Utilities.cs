
namespace Monopoly
{
    public static class Utilities
    {
        static Random r = new Random();

        public static void Shuffle<T>(this List<T> deck)
        {
            for (int i = deck.Count - 1; i > 0; --i)
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

        public static int RollD6() => r.Next(1, 7);

        public static bool TryGetValue<T>(this object obj, out T type)
        {
            type = default;
            if (obj is T) type = (T)obj;
            

            return obj is T;
        }
    }
}
