namespace Monopoly.Squares
{
    public class OwnableLand
    {
        public int Owner { get; set; }
        public int Cost { get; set; }
        public string Color { get; set; }
        public int Rent { get; set; }

        public int Mortgage { get; set; }

        public bool IsMortgaged { get; set; }
    }
}
