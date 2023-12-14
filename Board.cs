using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly
{
    public class Board
    {
        public Board()
        {
            // Load squares from somewhere 
            var ChanceCards = new int[10];
            ChanceCards.Shuffle();

            Queue<int> ChanceCardQueue = new Queue<int>(ChanceCards);

            var Properties = Utilities.ReadCSV("International Monopoly property Info.csv");
            Properties.ForEach(e =>
            {
                var cells = e.Split(',');

               // if (!string.IsNullOrEmpty(cells[3]))
                    test.Add(new Property(e));
            });
            // If 3rd value is missing then it is utility property
            // Will need to alter csv to include type and other squares

            
        }

        public List<ISquare> test = new List<ISquare>();

        public ISquare[] Squares = new ISquare[40];
        public List<Player> Players= new List<Player>();
        private int GoTouched;
        public void Play()
        {
            while(GoTouched != 2)
            {

                foreach(Player player in Players)
                {
                    // If pass the GO or lands on it =>
                    // Go touched plus 1
                    // If went to jail, GO doesn't count
                }

            }
        }
    }
}
