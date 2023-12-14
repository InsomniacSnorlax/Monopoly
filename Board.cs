using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly
{
    public class Board
    {
        public class EmptySquare : ISquare
        {
            public EmptySquare(SquareType Type, string Name) 
            { 
                this.Type = Type;
                this.Name = Name;
            }

            public string Name { get; }

            public SquareType Type { get; }

            public void Landed(Board board)
            {
                
            }
        }

        public Board()
        {
            // Load squares from somewhere 
            var ChanceCards = new int[10];
            ChanceCards.Shuffle();

            Queue<int> ChanceCardQueue = new Queue<int>(ChanceCards);

            AssignClass(Utilities.ReadCSV("International Monopoly property Info.csv"));

            Players.Add(new Player("Charlie"));
            Players.Add(new Player("Chau"));

            // If 3rd value is missing then it is utility property
            // Will need to alter csv to include type and other squares
            // Don't need jail
            // Turn jail.cs to GoToJail
            // Free parking does nothing so don't need it

        }

        public ISquare[] Squares = new ISquare[40];
        public List<Player> Players= new List<Player>();
        public Player currentPlayer;

        private bool FinishedGame = false; // Flaw is it will count each player touching Go
        public void Play()
        {
            Players.ForEach(e => e.Money = 1500);

            while (!FinishedGame)
            {

                foreach(Player player in Players)
                {
                    currentPlayer = player;
                    var movement = RollDice();

                    if (currentPlayer.IsInJail) continue;

                    currentPlayer.CurrentSqure += movement;
                    if (currentPlayer.CurrentSqure > 40)
                    {
                        currentPlayer.CurrentSqure %= 40;
                        currentPlayer.TouchedGo++;
                    }

                    if(currentPlayer.TouchedGo == 2)
                    {
                        FinishedGame = true;
                        break;
                    }

                    Squares[currentPlayer.CurrentSqure]?.Landed(this);

                }
            }
        }

        public void SendPlayerTo(string Name) => currentPlayer.CurrentSqure = Squares.ToList().FindIndex(e => e.Name == Name);

        public void SendPlayerTo(SquareType Type) => currentPlayer.CurrentSqure = Squares.ToList().FindIndex(e => e.Type == Type);

        public int RollDice()
        {
            Random r = new Random();
            int firstRoll = r.Next(1, 7);
            int secondRoll = r.Next(1, 7);

            if(firstRoll == secondRoll)
            {
                // Roll double == true
            }

            return firstRoll + secondRoll;
        }

        public bool PassGo()
        {

            return false;
        }

        public void AssignClass(List<string> strings)
        {
            for(int i = 0; i < strings.Count; i++)
            {
                var line = strings[i];

                var cells = line.Split(',');
                ISquare square = null;
                switch (cells[1])
                {
                    case "Property":
                        square = new Property(cells);
                        break;
                    case "Rail":
                        square = new Railroad(cells[0]);
                        break;
                    case "Utility":
                        square = new Utility(cells[0]);
                        break;
                    case "Chance":
                        square = new Cards(SquareType.Chance);
                        break;
                    case "Community":
                        square = new Cards(SquareType.Community);
                        break;
                    case "Jail":
                        square = new Jail(SquareType.Jail);
                        break;
                    case "GoToJail":
                        square = new Jail(SquareType.GoToJail);
                        break;
                    case "Tax":
                        square = new Tax(cells[0]);
                        break;
                    case "Go":
                        square = new EmptySquare(SquareType.Go, cells[0]);
                        break;
                    case "Parking":
                        square = new EmptySquare(SquareType.Parking, cells[0]);
                        break;
                }
                if (square == null) continue;
                Squares[i] = square;
            }
        }
    }
}
