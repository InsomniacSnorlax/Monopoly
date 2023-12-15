using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class OwnableLand : ISquare
    {
        public Player Owner { get; set; }
        public int Cost { get; set; }
        public string Color { get; set; }
        public int Rent { get; set; }

        public int Mortgage { get; set; }

        public bool IsMortgaged { get; set; }

        public string Name { get; set; }

        public virtual SquareType Type { get; set; }

        public int Position { get; set; }

        public void BuyProperty(Player player)
        {
            Owner = player;
            player.OwnedProperties.Add(this);
            player.Money -= Cost;
        }

        public void SellProperty()
        {
            Owner.Money += Cost / 2;
            Owner.OwnedProperties.Remove(this);
            Owner = null;
        }

        public int Mortgaged()
        {
            IsMortgaged = true;

            return Mortgage;
        }

        public void UnMortgage()
        {
            Owner.Money -= (int)(Mortgage * 1.1f);
            IsMortgaged = false;
        }

        public virtual void Landed(Board board)
        {
    
        }
    }
}
