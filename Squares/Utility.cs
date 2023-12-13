using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Utility : OwnableLand, ISquare
    {
        public int Position { get; }

        public string Name { get; }

        public SquareType Type { get; }

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
