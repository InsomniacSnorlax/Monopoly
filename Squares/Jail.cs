using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Jail : ISquare
    {
        public Jail(SquareType type, int position)
        {
            Type = type;
            Position = position;
        }

        public string Name => Type == SquareType.GoToJail ? "Go To Jail" : "Jail";
        public static int PrisonFine = 50;
        public int Position { get; }
        public SquareType Type { get; }

        public void Landed(Board board)
        {
            if (Type != SquareType.GoToJail) return;
            board.currentPlayer.CurrentSqure = board.SendPlayerTo(SquareType.Jail);
            board.currentPlayer.IsInJail = true;
        }
    }
}
