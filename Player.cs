using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly
{
    public class Player
    {
        public Player(string Name) => this.Name = Name;
        public string Name;
        public List<OwnableLand> OwnedProperties = new List<OwnableLand>();

        public int DiceRoll;
        public int CurrentSqure = 0;
        public int TouchedGo;
        public int TotalMoved;
        public int RolledDouble;
        public bool IsInJail;

        public bool IsBankrupted;
        public int JailFreeCards;
        public int Money
        {
            get => m_Money;
            set
            {
                if(value < 0) 
                {
                    SellAssets();
                }
                m_Money = value;
            }
        }

        private int m_Money;

        private void SellAssets()
        {
            // Have nothing left to sell hence broke
            while (Money < 0)
            {
                List<Property> Properties = new();
                List<OwnableLand> Land = new();

                OwnedProperties.FindAll(e =>
                {
                    var isProperty = e.TryGetValue<Property>(out Property p);
                    if (p != null) Properties.Add(p);
                    return isProperty;
                });
                var propertiesWithMortgages = OwnedProperties.FindAll(e =>
                {
                    if (e.TryGetValue<Property>(out Property p))
                        return p?.Houses == 0 && p.IsMortgaged == false;

                    return e.IsMortgaged == false;
                });

                var propertiesWithHouses = Properties.FindAll(e => e.Houses != 0);

                if (propertiesWithHouses.Count != 0)
                {
                    m_Money += propertiesWithHouses.MaxBy(e => e.Houses).SellHouse();
                }
                else if(propertiesWithMortgages.Count != 0)
                {
                    m_Money += propertiesWithMortgages.First().Mortgaged();
                }

                if(propertiesWithHouses.Count == 0 && propertiesWithMortgages.Count == 0 && m_Money <= 0)
                {
                    IsBankrupted = true;
                    break;
                }
            }
        }

        public void EscapePrison()
        {
            // Do your time or pay the fine
            if (Money < Jail.PrisonFine || !IsInJail)
                return;

            if (JailFreeCards > 0)
            {
                JailFreeCards--;
                IsInJail = false;
                return;
            }

            if(new Random().Next(0, 2) == 1)
            {
                Money -= Jail.PrisonFine;
                IsInJail = false;
            }
        }

        public void BuyProperty(OwnableLand property)
        {
            if (property.Cost < Money)
                property.BuyProperty(this);
        }
    }
}
