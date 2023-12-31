﻿using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Commands
{
    public class CommandRollDice : ICommand
    {
        public CommandRollDice(Player player)
        {
            this.player = player;
        }

        Player player;
        public int Result1;
        public int Result2;

        bool UsedJailFree;

        public void Execute()
        {
            Result1 = Utilities.RollD6();
            Result2 = Utilities.RollD6();

            if (Result1 == Result2)
            {
                CommandInvoker.Instance.rolled = false;
                player.RolledDouble++;
                player.IsInJail = false;
            }
            else player.RolledDouble = 0;
            player.DiceRoll = Result1 + Result2;

            if (player.JailFreeCards > 0 && player.IsInJail)
            {
                UsedJailFree = true;
                player.JailFreeCards--;
                player.IsInJail = false;
            }

            if (player.RolledDouble == 3)
            {
                CommandInvoker.Instance += new CommandGoToJail(player);
                CommandInvoker.Instance.rolled = true;
            }

            if (!player.IsInJail) CommandInvoker.Instance += new CommandMove(player, Board.Instance.Squares);

            player.DiceRollHistory.Enqueue(this);
        }

        public string Log()
        {
            string log = $"{player.Name} rolled: {Result1} {Result2}";
            if (player.IsInJail) log += $"\r\n{player.Name} is still in Jail";
            if (UsedJailFree) log += $"\r\n{player.Name} Used jail out of free card";
            return log;
        }
    }
}