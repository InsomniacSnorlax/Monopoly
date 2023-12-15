using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly
{
    public class Player
    {
        public Player(string Name) => this.Name = Name;
        public string Name;
        public List<ISquare> OwnedProperties = new List<ISquare>();
        
        public int CurrentSqure = 0;
        public int TouchedGo;
        public int TotalMoved;
        public int RolledDouble;
        public bool IsInJail;


        public int Money
        {
            get => m_Money;
            set
            {
                if(m_Money + value < 0) 
                {
                    SellAssets();
                }

                m_Money += value;
            }
        }

        private int m_Money;

        private void SellAssets()
        {
            // Have nothing left to sell hence broke
            if (OwnedProperties.Count <= 0) return;

            while (m_Money < 0)
            {
                List<Property> Properties = new();
                OwnedProperties.FindAll(e => e is Property).ForEach(e => Properties.Add(e as Property));
                if(Properties.FindAll(e => e.Houses > 0) != null)
                {
                    Properties.MaxBy(e => e.Houses)?.SellHouse();
                }
                else
                {

                }
            }
        }
    }
}
