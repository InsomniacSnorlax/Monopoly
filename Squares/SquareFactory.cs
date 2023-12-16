using Monopoly.Enums;
using Monopoly.Interfaces;
using static Monopoly.Main.Board;

namespace Monopoly.Squares
{
    public static class SquareFactory
    {
        public static ISquare CreateSquare(this string[] parameter)
        {
            ISquare square = null;
            int Position = int.Parse(parameter[3]) - 1;

            switch (parameter[1])
            {
                case "Property":
                    square = new Property(parameter);
                    break;
                case "Rail":
                    square = new Railroad(parameter);
                    break;
                case "Utility":
                    square = new Utility(parameter);
                    break;
                case "Tax":
                    square = new Tax(parameter);
                    break;
                case "Chance":
                    square = new Cards(SquareType.Chance, Position);
                    break;
                case "Community":
                    square = new Cards(SquareType.Community, Position);
                    break;
                case "Jail":
                    square = new Jail(SquareType.Jail, Position);
                    break;
                case "GoToJail":
                    square = new Jail(SquareType.GoToJail, Position);
                    break;
                case "Go":
                    square = new EmptySquare(SquareType.Go, Position);
                    break;
                case "Parking":
                    square = new EmptySquare(SquareType.Parking, Position);
                    break;
            }

            return square;
        }
    }
}
