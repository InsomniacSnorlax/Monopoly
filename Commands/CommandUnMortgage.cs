using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandUnMortgage : ICommand
    {
        public CommandUnMortgage(Player player, OwnableLand land)
        {
            this.player = player;
            this.land = land;
        }
        public Player player;
        public OwnableLand land;
        int mortgageAmount = 0;
        public void Execute()
        {
            mortgageAmount = land.UnMortgage();
            player.Money -= mortgageAmount;
        }

        public string Log()
        {
            return $"{player.Name} unmortgaged {land.Name} for {mortgageAmount}";
        }
    }
}
