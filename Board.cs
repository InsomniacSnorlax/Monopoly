using Monopoly.Enums;
using Monopoly.Interfaces;

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
