﻿using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public sealed class Cards : ISquare
    {
        public Cards(SquareType Type, int Position)
        {
            this.Type = Type;
            this.Position = Position;
        }
        public string Name => Type.ToString();
        public int Position { get; }
        public SquareType Type { get; }
        public void Landed()
        {
            CommandInvoker.Instance += new CommandPickCard(Type);
        }
    }
}