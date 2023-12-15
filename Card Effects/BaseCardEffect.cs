using Monopoly.Enums;
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
        public BaseCardEffect(string[] parameters) { 
            Text = parameters[0];
            this.parameters = parameters;
            cardType = parameters[3] == "Chance" ? SquareType.Chance : SquareType.Community;
        }

        public string Text { get; }
        protected string[] parameters;
        public SquareType cardType { get; }
        public virtual void PlayEffect()
        {

        }
    }
}
