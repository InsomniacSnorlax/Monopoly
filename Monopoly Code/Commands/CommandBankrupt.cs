using Monopoly.Interfaces;
using Monopoly.Main;
using System.Numerics;

namespace Monopoly.Commands
{
    public class CommandBankrupt : ICommand
    {
        public CommandBankrupt(Player player) => this.player = player;

        Player player;
        public void Execute()
        {
            player.IsBankrupted = true;
        }

        public string Log()
        {
            return $"{player.Name} is now Bankrupted and has lost the game";
        }
    }
}