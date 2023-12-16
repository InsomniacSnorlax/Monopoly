using Monopoly.Card_Effects;
using Monopoly.Commands;
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
        public int Turn;
        public int Rotation => Players.Max(e => e.TouchedGo);
        public void Play()
        {
            while (!FinishedGame)
            {
                Turn++;
                foreach (Player player in Players)
                {
                    currentPlayer = player;
                    if(!currentPlayer.IsBankrupted) CommandInvoker.Instance.State(currentPlayer);

                    if (currentPlayer.TouchedGo == 2) FinishedGame = true;
                }
            }
        }

        public int SendPlayerTo(SquareType Type) => Squares.ToList().FindIndex(e => e.Type == Type);
    }
}
