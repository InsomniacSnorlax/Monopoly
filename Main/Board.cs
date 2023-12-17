using Monopoly.Card_Effects;
using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;
using System.Text.RegularExpressions;

namespace Monopoly.Main
{
    public sealed class Board
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
            AssignClass(Utilities.ReadCSV(@"Data\Properties.csv"));
            AssignCard(Utilities.ReadCSV(@"Data\Cards.csv"));

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
        public Player CurrentPlayer;
        public Queue<ICard> ChanceCards = new();
        public Queue<ICard> CommunityCards = new();

        private bool FinishedGame = false;
        public int Turn;
        private int EndTour;
        public int Tours => Players.Max(e => e.Rotations);

        public void Init(List<Player> Players, int Tours)
        {
            this.Players = Players;
            EndTour = Tours;

            this.Players.ForEach(e => e.Money = 1500);
        }

        public void Play()
        {
            while (!FinishedGame)
            {
                Turn++;

                foreach (Player player in Players)
                {
                    CurrentPlayer = player;

                    if (CurrentPlayer.Rotations == EndTour || Players.FindAll(e => e.IsBankrupted).Count == Players.Count - 1)
                    {
                        FinishedGame = true;
                        break;
                    }

                    if (!CurrentPlayer.IsBankrupted) CommandInvoker.Instance.State(CurrentPlayer); 
                }
            }
        }
    }
}