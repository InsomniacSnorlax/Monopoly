using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Tax : ISquare
    {
        public Tax(string Name) => this.Name = Name;

        public string Name { get; }
        public int Cost { get; }
        public SquareType Type => SquareType.Tax;

        public void Landed(Board board)
        {
            board.currentPlayer.Money -= Cost;
        }
    }
}
