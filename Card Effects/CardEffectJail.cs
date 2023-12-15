using Monopoly.Enums;

namespace Monopoly.Card_Effects
{
    public class CardEffectJail : BaseCardEffect
    {
        public CardEffectJail(string[] parameters) : base(parameters)
        {
        }

        public override void PlayEffect()
        {
            Board.Instance.Squares.Find(e => e.Type == SquareType.GoToJail)?.Landed();
        }
    }
}
