using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Railroad : OwnableLand, ISquare
    {
        public int Position { get; }

        public string Name { get; }

        public SquareType Type => SquareType.Station;

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
