using Monopoly.Interfaces;

namespace Monopoly
{
    public class Player
    {
        public Player(string Name) => this.Name = Name;
        public string Name;
        public List<ISquare> OwnedProperties = new List<ISquare>();
        public int Money;
        public int CurrentSqure = 0;
        public int TouchedGo;
        public int TotalMoved;

        public bool IsInJail;
    }
}
