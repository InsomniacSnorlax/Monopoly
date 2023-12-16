using Monopoly.Interfaces;

namespace Monopoly.Commands
{
    public class CommandSellProperty : ICommand
    {
        public CommandSellProperty(Player player)
        {
            this.player = player;
        }

        Player player;

        public void Execute()
        {
            
        }

        public string Log()
        {
            return null;
        }
    }
}
