using Monopoly.Commands;
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
            CommandInvoker.Instance += new CommandGoToJail(Board.Instance.currentPlayer);
        }
    }
}
