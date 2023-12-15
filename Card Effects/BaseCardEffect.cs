using Monopoly.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Card_Effects
{
    public abstract class BaseCardEffect : ICard
    {
        public BaseCardEffect(ICard card, List<string> parameters) => this.card = card;

        protected ICard card;
        public string Text { get; }

        public virtual int PlayEffect()
        {
            card?.PlayEffect();

            return 0;
        }
    }
}
