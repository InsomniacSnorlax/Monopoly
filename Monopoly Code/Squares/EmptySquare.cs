using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public sealed class EmptySquare : ISquare
    {
        public EmptySquare(SquareType Type, int Position)
        {
            this.Type = Type;
            this.Position = Position;
        }

        public string Name => Type == SquareType.Go ? "Go" : "Free Parking";

        public SquareType Type { get; }

        public int Position { get; }

        public void Landed()
        {

        }
    }
}