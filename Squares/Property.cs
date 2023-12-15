
using Monopoly.Enums;
using Monopoly.Interfaces;
using System.Numerics;

namespace Monopoly.Squares
{
    [Serializable]
    public sealed class Property : OwnableLand
    {
        public override SquareType Type => SquareType.Property;

        public int Position { get; }
        public int BuildingCost { get; }

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
            Rent1= int.Parse(lines[5]);
            Rent2= int.Parse(lines[6]);
            Rent3 = int.Parse(lines[7]);
            Rent4= int.Parse(lines[8]);
            Rent5= int.Parse(lines[9]);
            BuildingCost = int.Parse(lines[10]);
            Mortgage = int.Parse(lines[11]);
        }

        public override void Landed(Board board)
        {
            Player currentPlayer = board.currentPlayer;

            if (Owner != null && currentPlayer != Owner && !IsMortgaged)
            {
                int rent = GetRent();
                bool CanAfford = currentPlayer.Money - rent > 0;

                if(CanAfford)
                {
                    Console.WriteLine($"{currentPlayer.Name} had to pay {Owner.Name}");
                    currentPlayer.Money -= rent;
                    Owner.Money += rent;
                }
            }
            if(Owner == null)
            {
                // Attempt to buy
                if(currentPlayer.Money > Cost)
                {
                    BuyProperty(currentPlayer);
                }
            }
        }



        public void SellHouse()
        {
            Owner.Money += BuildingCost / 2;
            Houses--;
        }

        public void BuyHouse()
        {
            if (Houses > 5) return;
            Owner.Money -= BuildingCost;
            Houses++;
        }

       

        public int GetRent()
        {
            // If only
            int rent = 0;

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
