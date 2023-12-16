using Monopoly.Interfaces;


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


        public void Execute()
        {
            Result1 = Utilities.RollD6();
            Result2 = Utilities.RollD6();
            IsSame = Result1 == Result2;
            PreviousRolledDouble = player.RolledDouble;

            if (IsSame)
            {
                player.RolledDouble++;
                player.IsInJail = false;
            }
            else player.RolledDouble = 0;
            player.DiceRoll = Result1 + Result2;
        }

        public string Log()
        {
            return $"{player.Name} rolled: {Result1} {Result2}";
        }
    }
}
