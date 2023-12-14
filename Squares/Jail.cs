using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Jail : ISquare
    {
        public Jail(SquareType type)
        {
            Type = type;
        }

        public string Name => Type == SquareType.GoToJail ? "Go To Jail" : "Jail";

        public SquareType Type { get; }

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
