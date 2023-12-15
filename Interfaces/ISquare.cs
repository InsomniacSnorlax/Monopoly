using Monopoly.Enums;

namespace Monopoly.Interfaces
{
    public interface ISquare
    {
        string Name { get; }
        SquareType Type { get; }
        int Position { get; }
        void Landed(Board board);
    }
}
