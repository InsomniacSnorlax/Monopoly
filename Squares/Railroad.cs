﻿using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public class Railroad : OwnableLand, ISquare
    {
        public Railroad(string name) 
        {
            Name = name;
        }

        public string Name { get; }

        public SquareType Type => SquareType.Rail;

        public void Landed(Board board)
        {
            Console.WriteLine(Type);
        }
    }
}
