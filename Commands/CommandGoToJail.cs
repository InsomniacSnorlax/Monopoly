using Monopoly.Enums;
using Monopoly.Interfaces;
using System.Numerics;

namespace Monopoly.Commands
{
    public class CommandGoToJail : ICommand
    {
        public int PreviousSquare;
        public Player player;

        public CommandGoToJail(Player player)
        {
            this.player = player;
        }

        public void Execute()
        {
            PreviousSquare = player.CurrentSqure;
            player.RolledDouble = 0;
            player.CurrentSqure = Board.Instance.SendPlayerTo(SquareType.Jail);
            player.IsInJail = true;
        }

        public string Log()
        {
            return $"{player.Name} unluckily found themselves in jail";
        }
    }
}
