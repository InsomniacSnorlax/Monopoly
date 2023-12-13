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

                if (string.IsNullOrEmpty(cells[3]))
                    new Utility();
                else
                    new Property();
            });
            // If 3rd value is missing then it is utility property
            // Will need to alter csv to include type and other squares
        }

        public int[] Squares = new int[40];
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
