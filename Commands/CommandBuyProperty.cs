using Monopoly.Interfaces;
using Monopoly.Squares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Commands
{
    public class CommandBuyProperty : ICommand
    {
        public CommandBuyProperty(Player player, OwnableLand land) { 
            this.player= player;
            this.land= land;
        }

        Player player;
        OwnableLand land;
        bool NoMoney;
        public void Execute()
        {
            if (player.Money < land.Cost || land.Owner != null)
                NoMoney = true;
            else
                land.BuyProperty(player);
        }

        public string Log()
        {
            return !NoMoney ? $"{player.Name} bought {land.Name} for {land.Cost}" : $"{player.Name} tried to buy {land.Name}";
        }
    }
}
