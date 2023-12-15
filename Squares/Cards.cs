using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Cards : ISquare
    {
        public Cards(SquareType Type, int Position)
        {
            this.Type = Type;
            this.Position = Position;
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
