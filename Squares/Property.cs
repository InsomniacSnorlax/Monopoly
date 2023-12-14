
using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    [Serializable]
    public sealed class Property : OwnableLand, ISquare
    {
        public SquareType Type => SquareType.Property;

        public string Name { get; set; }

        public int Position { get; }

        public int BuildingCost { get; }

        // Rent dependant on number of houses
        public int Houses { get; }
        public int Rent1 { get; }
        public int Rent2 { get; }
        public int Rent3 { get; }
        public int Rent4 { get; }
        public int Rent5 { get; }

        public Property(string[] lines)
        {
            Name = lines[0];
            Cost = int.Parse(lines[2]);
            Position = int.Parse(lines[3]);
            Color = lines[4];
            Rent1= int.Parse(lines[5]);
            Rent2= int.Parse(lines[6]);
            Rent3 = int.Parse(lines[7]);
            Rent4= int.Parse(lines[8]);
            Rent5= int.Parse(lines[9]);
            BuildingCost = int.Parse(lines[10]);
            Mortgage = int.Parse(lines[11]);
        }

        public void Landed(Board board)
        {
            if(Owner == null)
            {

            }
            else
            {

            }

            Console.WriteLine(Name);
        }

        public void BuyProperty(Player player)
        {
            Owner = player;
            player.Money -= Cost;
        }

        public void SellProperty()
        {
            Owner.Money += Cost / 2;
            Owner = null;
        }

        public float GetRent()
        {
            // If only
            float rent = 0;

            switch (Houses)
            {
                case 1:
                    rent = Rent1;
                    break;
                case 2:
                    rent = Rent2;
                    break;
                case 3:
                    rent = Rent3;
                    break;
                case 4:
                    rent = Rent4;
                    break;
                case 5:
                    rent = Rent5;
                    break;
            }

            return rent;
        }
    }
}
