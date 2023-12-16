using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandSellProperty : ICommand
    {
        public CommandSellProperty(Player player, OwnableLand property)
        {
            this.player = player;
            this.property = property;
        }

        Player player;
        OwnableLand property;
        public void Execute()
        {
            property.SellProperty();
        }

        public string Log()
        {
            return $"{player.Name} sold {property.Name} for ${property.Cost / 2}";
        }
    }
}
