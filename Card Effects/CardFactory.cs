using Monopoly.Interfaces;

namespace Monopoly.Card_Effects
{
    public static class CardFactory
    {
        public static ICard CreateCards(this string[] parameter)
        {
            string effect = parameter[4];
            ICard card = null;
            if (effect.ToLower().Contains("move")) card = new CardEffecfMove(parameter);
            else if (effect == "JailFree") card = new CardEffectJailFree(parameter);
            else if (effect == "GoToJail") card = new CardEffectJail(parameter);
            else if (effect == "Money") card = new CardEffectMoney(parameter);
            else if (effect == "Player") card = new CardEffectPlayerMoney(parameter);
            else if (effect == "Repair") card = new CardEffectRepair(parameter);

            return card;
        }
    }
}
