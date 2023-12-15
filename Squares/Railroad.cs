using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Railroad : OwnableLand
    {
        public override SquareType Type => SquareType.Rail;

        public Railroad(string name) 
        {
            Name = name;
        }

        public override void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
