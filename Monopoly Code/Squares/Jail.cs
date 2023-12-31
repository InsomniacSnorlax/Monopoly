﻿using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Squares
{
    public sealed class Jail : ISquare
    {
        public Jail(SquareType type, int position)
        {
            Type = type;
            Position = position;
        }

        public string Name => Type == SquareType.GoToJail ? "Go To Jail" : "Jail";
        public static int PrisonFine = 50;
        public int Position { get; }
        public SquareType Type { get; }

        public void Landed()
        {
            if (Type != SquareType.GoToJail) return;
            CommandInvoker.Instance += new CommandGoToJail(Board.Instance.CurrentPlayer);
        }
    }
}