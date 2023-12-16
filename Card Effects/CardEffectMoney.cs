using Monopoly.Main;

namespace Monopoly.Card_Effects
{
    public class CardEffectMoney : BaseCardEffect
    {
        public CardEffectMoney(string[] parameters) : base(parameters)
        {
            value = int.Parse(parameters[1]);
        }

        int value;

        public override void PlayEffect()
        {
            Board.Instance.currentPlayer.Money += value;
        }
    }
}
