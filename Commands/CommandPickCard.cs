using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Commands
{
    public class CommandPickCard : ICommand
    {
        public CommandPickCard(SquareType cardType)
        {
            this.cardType = cardType;
        }

        SquareType cardType;
        ICard card;
        public void Execute()
        {
            Queue<ICard> queue = cardType == SquareType.Community ? Board.Instance.CommunityCards : Board.Instance.ChanceCards;

            card = queue.Dequeue();

            card.PlayEffect();

            queue.Enqueue(card);
        }

        public string Log()
        {
            return $"{card.Text} of type {card.cardType} was picked";
        }
    }
}
