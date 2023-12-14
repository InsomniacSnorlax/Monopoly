using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Utility : OwnableLand, ISquare
    {
        public Utility(string name) => Name = name;

        public int Position { get; }

        public string Name { get; }

        public SquareType Type => SquareType.Utilities;

        public void Landed(Board board)
        {
            if(Owner != null && board.currentPlayer != Owner)
            {
                // Pay utlitiy based on how many the owner owns
            }
        }
    }
}
