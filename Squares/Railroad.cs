using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Railroad : OwnableLand
    {
        public override SquareType Type => SquareType.Rail;

        public Railroad(string[] lines)
        {
            Name = lines[0];
            Cost = int.Parse(lines[2]);
            Position = int.Parse(lines[3]);
            Rent = int.Parse(lines[7]);
            Rent2 = int.Parse(lines[8]);
            Rent3 = int.Parse(lines[9]);
            Rent4 = int.Parse(lines[10]);
            Mortgage = int.Parse(lines[13]);
        }

        int Rent2;
        int Rent3;
        int Rent4;

        public override int GetRent()
        {
            int Ammount = Owner.OwnedProperties.FindAll(e => e.Type == Type).Count;
            int Rent = this.Rent;

            if (Rent == 2) Rent = Rent2;
            if (Rent == 3) Rent = Rent3;
            if (Rent == 4) Rent = Rent4;

            return Rent;
        }
    }
}
