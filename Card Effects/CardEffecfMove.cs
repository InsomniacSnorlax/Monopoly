using Monopoly.Enums;
using Monopoly.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Card_Effects
{
    public class CardEffecfMove : BaseCardEffect
    {
        public CardEffecfMove(ICard card, List<string> parameters) : base(card, parameters)
        {
            int Positon = 0;

            if(parameters[2] == "Rail" || parameters[2] == "Utility")
            {
                SquareType type = parameters[2] == "Rail" ? SquareType.Rail : SquareType.Utilities;

                var squares = Board.Instance.Squares.ToList().FindAll(e => e.Type == type);

                //for
            }
        }
    }
}
