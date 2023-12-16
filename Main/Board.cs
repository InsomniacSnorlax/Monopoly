using Monopoly.Card_Effects;
using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;
using System.Text.RegularExpressions;

namespace Monopoly.Main
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
            AssignClass(Utilities.ReadCSV(@"\Properties.csv"));
            AssignCard(Utilities.ReadCSV(@"\Cards.csv"));

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
        public List<Player> Players = new();
        public Player currentPlayer;
        public Queue<ICard> ChanceCards = new();
        public Queue<ICard> CommunityCards = new();
        private bool FinishedGame = false;
        public int Turn;

        private int EndTurn;
        public int Rotation => Players.Max(e => e.TouchedGo);

        public void Init(List<Player> Players, int Turns)
        {
            this.Players = Players;
            EndTurn = Turns;

            this.Players.ForEach(e => e.Money = 1500);
        }

        public void Play()
        {
            while (!FinishedGame)
            {
                Turn++;

                foreach (Player player in Players)
                {
                    currentPlayer = player;

                    if (currentPlayer.TouchedGo == EndTurn || Players.FindAll(e => e.IsBankrupted).Count == Players.Count - 1)
                    {
                        FinishedGame = true;
                        break;
                    }

                    if (!currentPlayer.IsBankrupted) CommandInvoker.Instance.State(currentPlayer); 
                }
            }
        }

        public int SendPlayerTo(SquareType Type) => Squares.ToList().FindIndex(e => e.Type == Type);
    }
}
