using Monopoly.Interfaces;
using Monopoly.Squares;


namespace Monopoly.Commands
{
    public class CommandSellHouse : ICommand
    {
        public CommandSellHouse(Player player, Property property)
        {
            this.player = player;
            this.property = property;
        }
        Player player;
        Property property;

        bool NoSell;

        public void Execute()
        {
            var colorProperties = player.OwnedProperties.FindAll(e => e.Color == property.Color);

            var highest = colorProperties.Max(e => {
                e.TryGetValue(out Property property);
                return property.Houses;
            });

            if (property.Houses == highest)
            {
                property.SellHouse();
                return;
            }

            NoSell = true;
        }

        public string Log()
        {
            return !NoSell ? $"{player.Name} sold {property.Name} for {property.BuildingCost / 2}" : $"{player.Name} couldn't sell house/hotel from {property.Name}";
        }
    }
}
