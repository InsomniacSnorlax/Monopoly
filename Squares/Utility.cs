using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Utility : OwnableLand, ISquare
    {
        public int Position => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public SquareType Type => throw new NotImplementedException();

        public void Landed(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
