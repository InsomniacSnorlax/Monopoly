
using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
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

        public Property(string text)
        {
            var lines = text.Split(',');
            Name = lines[0];
            //Cost = int.Parse(lines[2]);
            /*
            Position = int.Parse(lines[3]);
            Color = lines[4];
            Rent1= int.Parse(lines[5]);
            Rent2= int.Parse(lines[6]);
            Rent3 = int.Parse(lines[7]);
            Rent4= int.Parse(lines[8]);
            Rent5= int.Parse(lines[9]);
            BuildingCost = int.Parse(lines[10]);
            Mortgage= int.Parse(lines[11]);*/
        }

        public void Landed(Board board)
        {
            Console.WriteLine(Name);
        }
    }
}
