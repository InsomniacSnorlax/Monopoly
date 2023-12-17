using Monopoly.Interfaces;
using Monopoly.Main;
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

        static bool reset = true;
        public bool rolled;
        public void State(Player player)
        {
            var playerTurn = true;
            rolled = false;
            while (playerTurn)
            {
                if(reset) Hotkeys(player, rolled);
                var line = Console.ReadLine().ToLower();
                reset = false;
                if (line == "r" && rolled == false) {
                    CommandInvoker.Instance += new CommandRollDice(player);
                    rolled = true;
                }

                if (line == "f" && player.IsInJail) CommandInvoker.Instance += new CommandPayFine(player);

                if (line == "b" && Board.Instance.Squares[player.CurrentSqure].TryGetValue(out OwnableLand land)) CommandInvoker.Instance += new CommandBuyProperty(player, land);

                if (line == "v") CommandInvoker.Instance += new CommandSelectProperty(player);

                if (line == "bankrupt")
                {
                    Console.Clear();
                    CommandInvoker.Instance += new CommandBankrupt(player);
                    playerTurn = false;
                } 

                if (line == "end")
                {
                    Console.Clear();
                    playerTurn = false;
                }
            }
            reset = true;
            // Player's turn
            // Roll dice
            // Finish turn
            // If only unowned property have selection to by
            // Buy house for property
            // 
        }

        public static CommandInvoker operator +(CommandInvoker origin, ICommand command)
        {
            reset = true;
            Console.Clear();
            origin.Logs.Enqueue(command);
            command.Execute();
            Console.WriteLine(command.Log());
            Console.WriteLine();
            return origin;
        }

        private void Hotkeys(Player player, bool rolled)
        {
            Console.WriteLine($"Current Turn: {Board.Instance.Turn}");
            Console.WriteLine($"Time around the map: {Board.Instance.Tours}");
            Console.WriteLine($"Currently on: {Board.Instance.Squares[player.CurrentSqure].Name}");
            if(Board.Instance.Squares[player.CurrentSqure].TryGetValue(out OwnableLand land)) Console.WriteLine($"Cost: {land.Cost}");
            if (Board.Instance.Squares[player.CurrentSqure].TryGetValue(out Property prop)) Console.WriteLine($"Color: {prop.Color}");
            Console.WriteLine();
            if(!rolled) Console.WriteLine("R to roll dice");
            if (player.IsInJail) Console.WriteLine("F to be released from jail");
            if (land != null && land.Owner == null) Console.WriteLine("B to buy land");
            Console.WriteLine("V to select owned Properties");
            Console.WriteLine("Bankrupt to forfeit the game");
            Console.WriteLine("End to end turn");
            Console.WriteLine();
            Console.WriteLine($"{player.Name}'s turn");
            Console.WriteLine($"Bank {player.Money}");
            Console.WriteLine();
            Console.WriteLine("Waiting for input....");
        }
    }
}