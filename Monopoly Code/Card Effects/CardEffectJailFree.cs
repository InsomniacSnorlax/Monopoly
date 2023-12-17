using Monopoly.Main;

namespace Monopoly.Card_Effects
{
    public class CardEffectJailFree : BaseCardEffect
    {
        public CardEffectJailFree(string[] parameters) : base(parameters)
        {
        }

        public override void PlayEffect()
        {
            Board.Instance.CurrentPlayer.JailFreeCards++;
        }
    }
}