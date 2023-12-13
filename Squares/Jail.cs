using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Jail : ISquare
    {
        public string Name { get; }

        public SquareType Type { get; }

        public int Position { get; }

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
