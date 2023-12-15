using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;

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


        public class EmptySquare : ISquare
        {
            public EmptySquare(SquareType Type, int Position) 
            { 
                this.Type = Type;
                this.Position = Position;
            }

            public string Name => Type == SquareType.Go ? "Go" : "Free Parking";

            public SquareType Type { get; }

            public int Position { get; }

            public void Landed(Board board)
            {
                
            }
        }

        public Board()
        {
            var ChanceCards = new int[10];
            ChanceCards.Shuffle();

            Queue<int> ChanceCardQueue = new Queue<int>(ChanceCards);

            AssignClass(Utilities.ReadCSV("International Monopoly property Info.csv"));
            //AssignCard(Utilities.ReadCSV("Cards.csv"));


            Players.Add(new Player("Charlie"));
            Players.Add(new Player("Chau"));

            Players.ForEach(e => e.Money = 200);
        }

        public List<ISquare> Squares = new();
        public List<Player> Players= new();
        public Player currentPlayer;
        public Queue<Cards> ChanceCards = new();
        public Queue<Cards> CommunityCards = new();


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
                    currentPlayer.EscapePrison();

                    if (currentPlayer.IsInJail) continue;

                    currentPlayer.CurrentSqure += movement;
                    if (currentPlayer.CurrentSqure > Squares.Count - 1)
                    {
                        currentPlayer.CurrentSqure %= (Squares.Count - 1);
                        currentPlayer.TouchedGo++;
                        //currentPlayer.Money += 200;
                    }

                    if(currentPlayer.TouchedGo == 4)
                    {
                        FinishedGame = true;
                        break;
                    }
                    Console.WriteLine($"Current Index: {currentPlayer.Name} {currentPlayer.CurrentSqure}");
                    Squares[currentPlayer.CurrentSqure]?.Landed(this);

                }
            }
        }



        //Effects
        // Go -> collect if
        // Move
        // Pay
        // Jail Free
        // Repairs -> pay per house
        // Player -> Get money from each player
        // Nearest
        // Dice

        public void SendPlayerTo(string Name) => currentPlayer.CurrentSqure = Squares.ToList().FindIndex(e => e.Name == Name);

        public int SendPlayerTo(SquareType Type) => Squares.ToList().FindIndex(e => e.Type == Type);

        //public int SendPlayerTo(ISquare Square) => Squares.to

        public int RollDice()
        {
            Random r = new Random();
            int firstRoll = r.Next(1, 7);
            int secondRoll = r.Next(1, 7);

            if(firstRoll == secondRoll)
            {
                currentPlayer.IsInJail = false;
            }
            else
            {
                currentPlayer.RolledDouble++;
            }

            return firstRoll + secondRoll;
        }

        public bool PassGo()
        {

            return false;
        }

        public void AssignClass(List<string> strings)
        {
            for (int i = 0; i < strings.Count; i++)
            {
                var line = strings[i];

                var cells = line.Split(',');
                ISquare square = null;
                int Position = int.Parse(cells[3]) - 1;

                switch (cells[1])
                {
                    case "Property":
                        square = new Property(cells);
                        break;
                    case "Rail":
                        square = new Railroad(cells[0],Position);
                        break;
                    case "Utility":
                        square = new Utility(cells[0], Position);
                        break;
                    case "Chance":
                        square = new Cards(SquareType.Chance, Position);
                        break;
                    case "Community":
                        square = new Cards(SquareType.Community, Position);
                        break;
                    case "Jail":
                        square = new Jail(SquareType.Jail, Position);
                        break;
                    case "GoToJail":
                        square = new Jail(SquareType.GoToJail, Position);
                        break;
                    case "Tax":
                        square = new Tax(cells[0], Position);
                        break;
                    case "Go":
                        square = new EmptySquare(SquareType.Go, Position);
                        break;
                    case "Parking":
                        square = new EmptySquare(SquareType.Parking, Position);
                        break;
                }
                if (square == null) continue;
                Squares.Add(square);
            }

            Squares.OrderBy(e => e.Position);
        }

        public void AssignCard(List<string> strings)
        {

        }
    }
}
