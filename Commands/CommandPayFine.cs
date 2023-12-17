using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    internal class CommandPayFine : ICommand
    {
        public CommandPayFine(Player player)
        {
            this.player = player;
        }

        Player player;

        public void Execute()
        {
            player.Money -= Jail.PrisonFine;
            player.IsInJail = false;
        }

        public string Log()
        {
            return $"{player.Name} payed {Jail.PrisonFine} to be released from jail";
        }
    }
}