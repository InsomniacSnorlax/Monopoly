using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandRent : ICommand
    {
        public CommandRent(OwnableLand property, Player player)
        {
            this.property = property;
            this.player = player;
        }

        OwnableLand property;
        Player player;
        int rent;
        public void Execute()
        {
            rent = property.GetRent();

            property.PayRent(player, rent);
        }

        public string Log()
        {
            return $"{player.Name} had to pay ${rent} to {property.Owner.Name}";
        }
    }
}