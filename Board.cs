using Monopoly.Card_Effects;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;
using System.Text.RegularExpressions;

namespace Monopoly
{
    public class Board
    {
        public static Board Instance
        {
            get
            {
                if (m_Instance == null) m_Instance = new Board();

                return m_Instance;
            }

        }

        private static Board m_Instance;

        public Board()
        {
            AssignClass(Utilities.ReadCSV("International Monopoly property Info.csv"));
            AssignCard(Utilities.ReadCSV("Cards.csv"));

            Players.Add(new Player("Charlie"));
            Players.Add(new Player("Chau"));
            Players.ForEach(e => e.Money = 1500);

            void AssignClass(List<string> strings)
            {
                strings.ForEach(e => Squares.Add(e.Split(',').CreateSquare()));
                Squares.OrderBy(e => e.Position);
            }

            void AssignCard(List<string> strings)
            {
                List<ICard> cards = new List<ICard>();
                strings.ForEach(e => cards.Add(Regex.Split(e, "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").CreateCards()));

                for (int i = 0;i < cards.Count; i++)
                {
                    Console.WriteLine(i);
                }

                var community = cards.FindAll(e => e.cardType == SquareType.Community);
                var chance = cards.FindAll(e => e.cardType == SquareType.Chance);

                community.Shuffle();
                chance.Shuffle();

                CommunityCards = new Queue<ICard>(community);
                ChanceCards = new Queue<ICard>(chance);
            }
        }

        public List<ISquare> Squares = new();
        public List<Player> Players= new();
        public Player currentPlayer;
        public Queue<ICard> ChanceCards = new();
        public Queue<ICard> CommunityCards = new();
        private bool FinishedGame = false;

        public void Play()
        {
            while (!FinishedGame)
            {
                foreach(Player player in Players)
                {
                    currentPlayer = player;
                    if (currentPlayer.IsBankrupted) continue;

                    var movement = RollDice();

                    if (currentPlayer.IsInJail) continue;

                    currentPlayer.CurrentSqure += movement;

                    if (currentPlayer.CurrentSqure > Squares.Count - 1)
                    {
                        currentPlayer.CurrentSqure %= (Squares.Count - 1);
                        currentPlayer.TouchedGo++;
                        currentPlayer.Money += 200;
                    }

                    if(currentPlayer.TouchedGo == 4)
                    {
                        FinishedGame = true;
                        break;
                    }
                   
                    Squares[currentPlayer.CurrentSqure]?.Landed();
                    Console.WriteLine($"Current Index: {currentPlayer.Name} {currentPlayer.CurrentSqure} {currentPlayer.Money}");
                }
            }
        }

        public void SendPlayerTo(string Name) => currentPlayer.CurrentSqure = Squares.ToList().FindIndex(e => e.Name == Name);

        public int SendPlayerTo(SquareType Type) => Squares.ToList().FindIndex(e => e.Type == Type);


        public int RollDice()
        {
            if(currentPlayer.IsInJail) currentPlayer.EscapePrison(); // Will attempt to leave jail

            int firstRoll = Utilities.RollD6();
            int secondRoll = Utilities.RollD6();

            if (firstRoll == secondRoll)
            {
                currentPlayer.IsInJail = false;
            }
            else
            {
                currentPlayer.RolledDouble++;

                if (currentPlayer.RolledDouble == 3) Squares.Find(e => e.Type == SquareType.GoToJail)?.Landed();
            }

            return firstRoll + secondRoll;
        }
    }
}
