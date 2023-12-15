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
            var ChanceCards = new int[10];
            ChanceCards.Shuffle();

            Queue<int> ChanceCardQueue = new Queue<int>(ChanceCards);

            AssignClass(Utilities.ReadCSV("International Monopoly property Info.csv"));
            //AssignCard(Utilities.ReadCSV("Cards.csv"));


            Players.Add(new Player("Charlie"));
            Players.Add(new Player("Chau"));

            Players.ForEach(e => e.Money = 1500);
        }

        public ISquare[] Squares = new ISquare[40];
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
                    var movement = RollDice();

                    if (currentPlayer.IsInJail) continue;

                    currentPlayer.CurrentSqure += movement;
                    if (currentPlayer.CurrentSqure > Squares.Length - 1)
                    {
                        currentPlayer.CurrentSqure %= (Squares.Length - 1);
                        currentPlayer.TouchedGo++;
                    }

                    if(currentPlayer.TouchedGo == 2)
                    {
                        FinishedGame = true;
                        break;
                    }
                    Console.WriteLine($"Current Index: {currentPlayer.CurrentSqure}");
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

        public void SendPlayerTo(SquareType Type) => currentPlayer.CurrentSqure = Squares.ToList().FindIndex(e => e.Type == Type);

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

        public void AssignCard(List<string> strings)
        {

        }
    }
}
