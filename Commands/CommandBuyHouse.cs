using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandBuyHouse : ICommand
    {
        public CommandBuyHouse(Player player, Property property) {
            this.player = player;
            this.property = property;
        }
        Player player;
        Property property;

        int houses;
        bool NoPurchase;
        public void Execute()
        {
            var colorProperties = player.OwnedProperties.FindAll(e => e.Color == property.Color);

            var lowest = colorProperties.Min(e => {
                e.TryGetValue(out Property property);
                return property.Houses;
            });

            houses = property.Houses + 1;

            if (property.Houses == 4 && Bank.Instance.Hotels <= 0 || property.Houses < 4 && Bank.Instance.Houses <= 0)
            {
                // No more hotels to buy
                NoPurchase = true;
                return;
            }

            if (property.Houses + 1 - lowest < 2)
            {
                property.BuyHouse();
                
                return;
            }
            NoPurchase = true;
        }

        public string Log()
        {
            string housing = houses == 5 ? "Hotel" : "House";
            return !NoPurchase ? $"{player.Name} bought a {housing} for {property.Name} for {property.BuildingCost}" : $"{player.Name} cound't buy a {housing} for {property.Name}";
        }
    }
}