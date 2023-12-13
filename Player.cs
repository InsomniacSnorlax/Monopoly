using Monopoly.Interfaces;

namespace Monopoly
{
    public class Player
    {
        public List<ISquare> OwnedProperties = new List<ISquare>();
        public int Money;
    }
}
