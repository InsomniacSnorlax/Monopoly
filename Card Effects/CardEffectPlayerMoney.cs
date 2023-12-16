using Monopoly.Main;

namespace Monopoly.Card_Effects
{
    public class CardEffectPlayerMoney : BaseCardEffect
    {
        public CardEffectPlayerMoney(string[] parameters) : base(parameters)
        {
            value = int.Parse(parameters[1]);
        }

        int value;

        public override void PlayEffect()
        {
            Player currentPlayer = Board.Instance.currentPlayer;

            currentPlayer.Money += value;

            Board.Instance.Players.ForEach(e => {
                if (e != currentPlayer) e.Money += -value;
            });
        }
    }
}
