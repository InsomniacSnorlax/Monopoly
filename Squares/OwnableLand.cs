namespace Monopoly.Squares
{
    public class OwnableLand
    {
        public int Owner { get; }
        public int Cost { get; }
        public string? Color { get; }
        public int Rent { get; }

        public int Mortgage { get; }

        public bool IsMortgaged { get; set; }
    }
}
