using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Cards : ISquare
    {
        public Cards(SquareType type) {
            Type = type;
        }
        public string Name => Type.ToString();

        public SquareType Type { get; }

        public int Position { get; }

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
