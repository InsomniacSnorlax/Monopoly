using Monopoly.Enums;

namespace Monopoly.Interfaces
{
    public interface ICard
    {
        public string Text { get; }
        public SquareType cardType { get; }
        public void PlayEffect();
    }
}