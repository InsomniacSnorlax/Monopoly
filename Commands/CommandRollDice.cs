using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Commands
{
    public class CommandRollDice : ICommand
    {
        public CommandRollDice(Player player) {
            this.player = player;
        }

        Player player;
        int Result1;
        int Result2;

        bool IsSame;
        int PreviousRolledDouble;
        bool UsedJailFree;

        public void Execute()
        {
            if(player.JailFreeCards > 0)
            {
                UsedJailFree= true;
                player.JailFreeCards--;
            }

            Result1 = Utilities.RollD6();
            Result2 = Utilities.RollD6();
            IsSame = Result1 == Result2;
            PreviousRolledDouble = player.RolledDouble;

            if (IsSame)
            {
                CommandInvoker.Instance.rolled = false;
                player.RolledDouble++;
                player.IsInJail = false;
            }
            else player.RolledDouble = 0;
            player.DiceRoll = Result1 + Result2;

            if (player.RolledDouble == 3) {
                CommandInvoker.Instance += new CommandGoToJail(player);
                CommandInvoker.Instance.rolled = true;
            } 

            if (!player.IsInJail) CommandInvoker.Instance += new CommandMove(player);
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
