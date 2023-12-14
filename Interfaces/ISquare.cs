using Monopoly.Enums;

namespace Monopoly.Interfaces
{
    public interface ISquare
    {
       // int Position { get; }
        string Name { get; }
        SquareType Type { get; }
        void Landed(Board board);
    }
}
