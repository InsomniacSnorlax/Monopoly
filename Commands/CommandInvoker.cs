using Monopoly.Interfaces;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandInvoker
    {
        public static CommandInvoker Instance
        {
            get
            {
                if (m_Instance == null) m_Instance = new CommandInvoker();

                return m_Instance;
            }
            set
            {
                m_Instance = value;
            }
        }

        private static CommandInvoker m_Instance;

        public Queue<ICommand> Logs = new Queue<ICommand>();
        bool playerTurn;
        public void State(Player player, string readLine)
        {
            playerTurn = true;

            while (playerTurn)
            {
                var line = Console.ReadLine();

                if (readLine == "R") CommandInvoker.Instance += new CommandRollDice(player);

                if (player.RolledDouble == 3) CommandInvoker.Instance += new CommandGoToJail(player);

                if (!player.IsInJail) CommandInvoker.Instance += new CommandMove(player);

                if (readLine == "B" && Board.Instance.Squares[player.CurrentSqure].TryGetValue(out OwnableLand land) && land.Owner == null) CommandInvoker.Instance += new CommandBuyProperty(player, land);

                if (line == "End") playerTurn = false;
            }

            // Player's turn

            CommandInvoker.Instance += new CommandGoToJail(player);

            // Roll dice
            // Finish turn
            // If only unowned property have selection to by
            // Buy house for property
            // 
        }

        public static CommandInvoker operator +(CommandInvoker origin, ICommand command)
        {
            origin.Logs.Enqueue(command);
            command.Execute();
            Console.WriteLine(command.Log());
            return origin;
        }
    }
}
