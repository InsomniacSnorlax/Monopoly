using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Commands
{
    public sealed class CommandTax : ICommand
    {
        public CommandTax(Player player, int cost) { 
            this.player = player;
            this.cost = cost;
        }
        Player player;
        int cost;
        public void Execute()
        {
            player.Money -= cost;
        }

        public string Log()
        {
            return $"{player.Name} was taxed of ${cost}";
        }
    }
}