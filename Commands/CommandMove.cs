using Monopoly.Interfaces;
using Monopoly.Main;

namespace Monopoly.Commands
{
    public class CommandMove : ICommand
    {
        public CommandMove(Player player, List<ISquare> Squares)
        {
            this.player = player;
            this.Squares = Squares;
        }

        Player player;
        int PreviousCurrentSquare;
        ISquare LandedSquare;
        bool touchedGo;
        int previousMoney;
        List<ISquare> Squares;
        public void Execute()
        {
            PreviousCurrentSquare = player.CurrentSqure;

            player.CurrentSqure += player.DiceRoll;

            player.DiceRoll = 0;

            previousMoney = player.Money;

            if (player.CurrentSqure > Squares.Count - 1)
            {
                player.CurrentSqure %= (Squares.Count - 1);
                player.TouchedGo++;
                player.Money += 200;
                touchedGo = true;
            }

            LandedSquare = Squares[player.CurrentSqure];

            LandedSquare.Landed();
        }

        public string Log()
        {
            string log = $"{player.Name} landed on {LandedSquare.Name}";
            if(touchedGo)
            {
                log += $"\r\n{player.Name} touched Go and was awarded $200 -> {previousMoney} to {previousMoney += 200}";
            }

            return log;
        }
    }
}
