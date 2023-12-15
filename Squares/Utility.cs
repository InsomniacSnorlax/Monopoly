using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Utility : OwnableLand
    {
        public override SquareType Type => SquareType.Utilities;
        public Utility(string Name, int Position)
        {
            this.Name = Name;
            this.Position = Position;
        }


        public override void Landed(Board board)
        {
            if(Owner != null && board.currentPlayer != Owner)
            {
                // Pay utlitiy based on how many the owner owns
            }
        }
    }
}
