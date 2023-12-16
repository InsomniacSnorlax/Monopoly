using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Card_Effects
{
    internal class CardEffectRepair : BaseCardEffect
    {
        public CardEffectRepair(string[] parameters) : base(parameters)
        {
            var split = parameters[1].Split("|");

            houseCosts = int.Parse(split[0]);
            hotelCosts = int.Parse(split[1]);

        }

        int houseCosts;
        int hotelCosts;

        public override void PlayEffect()
        {
            Player currentPlayer = Board.Instance.currentPlayer;
            int totalHouses = 0;
            int totalHotels = currentPlayer.OwnedProperties.FindAll(e => e.TryGetValue<Property>(out Property property) && property.Houses == 5).Count;

            currentPlayer.OwnedProperties.ForEach(e =>
            {
                if (e.TryGetValue<Property>(out Property property) && property.Houses < 5) totalHouses += property.Houses;
            });

            int totalCost = hotelCosts * totalHotels + houseCosts * totalHouses;

            currentPlayer.Money -= totalCost;
        }
    }
}
