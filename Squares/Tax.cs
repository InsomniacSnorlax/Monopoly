using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Squares
{
    public class Tax : ISquare
    {
        public Tax(string Name, int Position)
        {
            this.Name = Name;
            this.Position = Position;
        }

        public string Name { get; }
        public int Cost { get; }
        public SquareType Type => SquareType.Tax;

        public int Position { get; }

        public void Landed()
        {
            CommandInvoker.Instance += new CommandTax(Board.Instance.currentPlayer, Cost);
        }
    }
}
