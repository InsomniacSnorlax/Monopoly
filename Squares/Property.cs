
using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Property : OwnableLand, ISquare
    {
        public SquareType Type => SquareType.Property;

        public string Name => throw new NotImplementedException();

        public int Position => throw new NotImplementedException();

        public int BuildingCost { get; }

        // Rent dependant on number of houses
        public int Houses { get; }
        public int Rent1 { get; }
        public int Rent2 { get; }
        public int Rent3 { get; }
        public int Rent4 { get; }
        public int Rent5 { get; }

        public void Landed(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
