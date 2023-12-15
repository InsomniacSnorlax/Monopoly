using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Railroad : OwnableLand
    {
        public override SquareType Type => SquareType.Rail;

        public Railroad(string Name, int Position)
        {
            this.Name = Name;
            this.Position = Position;
        }

        public override void Landed(Board board)
        {

        }
    }
}
