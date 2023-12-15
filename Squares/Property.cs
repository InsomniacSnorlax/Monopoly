
using Monopoly.Enums;
using Monopoly.Interfaces;
using System.Numerics;

namespace Monopoly.Squares
{
    [Serializable]
    public sealed class Property : OwnableLand
    {
        public override SquareType Type => SquareType.Property;
        public int BuildingCost { get; }

        public int ColorRent { get; }

        // Rent dependant on number of houses
        public int Houses { get; set; }
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

            Rent = int.Parse(lines[5]);
            ColorRent = int.Parse(lines[6]);

            Rent1 = int.Parse(lines[7]);
            Rent2 = int.Parse(lines[8]);
            Rent3 = int.Parse(lines[9]);
            Rent4 = int.Parse(lines[10]);
            Rent5 = int.Parse(lines[11]);
            BuildingCost = int.Parse(lines[12]);
            Mortgage = int.Parse(lines[13]);
        }


        public int SellHouse()
        {
            Console.WriteLine($"House price {BuildingCost / 2}");
            Houses--;
            return BuildingCost / 2;
        }

        public void BuyHouse()
        {
            if (Houses > 5) return;
            Owner.Money -= BuildingCost;
            Houses++;
        }

        public override int GetRent()
        {
            // If only
            int rent = Owner.OwnedProperties.FindAll(e => e.Color == Color).Count == 3 ? ColorRent : Rent;

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
