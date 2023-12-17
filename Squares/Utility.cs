using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public sealed class Utility : OwnableLand
    {
        public override SquareType Type => SquareType.Utilities;
        public Utility(string[] lines)
        {
            Name = lines[0];
            Cost = int.Parse(lines[2]);
            Position = int.Parse(lines[3]);
            Color = lines[4];

            Rent1 = int.Parse(lines[7]);
            Rent2 = int.Parse(lines[8]);

            Mortgage = int.Parse(lines[13]);
        }

        // Rent dependant on amount of Utilities owned
        int Rent1;
        int Rent2;

        public override int GetRent() => Owner.OwnedProperties.FindAll(e => e.Type == Type).Count == 2 ? Rent2 : Rent1;
    }
}