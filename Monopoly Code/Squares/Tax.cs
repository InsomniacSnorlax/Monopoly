using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Squares
{
    public sealed class Tax : ISquare
    {
        public Tax(string[] parameter)
        {
            this.Name = parameter[0];
            this.Position = int.Parse(parameter[3]);
            this.Cost = int.Parse(parameter[5]);
        }

        public string Name { get; }
        
        public SquareType Type => SquareType.Tax;

        public int Position { get; }
        public int Cost { get; }
        public void Landed()
        {
            CommandInvoker.Instance += new CommandTax(Board.Instance.CurrentPlayer, Cost);
        }
    }
}