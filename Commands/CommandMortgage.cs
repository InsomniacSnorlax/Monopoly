using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandMortgage : ICommand
    {
        public CommandMortgage(Player player, OwnableLand land)
        {
            this.player = player;
            this.land = land;
        }
        public Player player;
        public OwnableLand land;
        int mortgageAmount = 0;
        public void Execute()
        {
            mortgageAmount = land.Mortgaged();
            player.Money += mortgageAmount;
        }

        public string Log()
        {
            return $"{player.Name} mortgaged {land.Name} for {mortgageAmount}";
        }
    }
}
