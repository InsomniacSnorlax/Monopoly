using Monopoly.Enums;

namespace Monopoly.Interfaces
{
    public interface ISquare
    {
        string Name { get; }
        SquareType Type { get; }
        void Landed(Board board);
    }
}
