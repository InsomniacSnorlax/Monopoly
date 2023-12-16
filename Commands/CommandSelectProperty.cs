using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;

namespace Monopoly.Commands
{
    public class CommandSelectProperty : ICommand
    {
        public CommandSelectProperty(Player player) 
        {
            this.player = player;
        }
        Player player;
        OwnableLand property;
        public void Execute()
        {
            bool exit = false;


            Console.WriteLine("Type Esc to return to menu");
            for (int i = 0; i < player.OwnedProperties.Count; i++)
            {
                Console.WriteLine($"{i} {player.OwnedProperties[i].Name}");
            }

            while (!exit)
            {
                var selection = Console.ReadLine();
                if (selection.ToLower() == "esc")
                {
                    Console.Clear();
                    exit = true;
                }
                if (int.TryParse(selection, out int index))
                {
                    exit = true;
                    property = player.OwnedProperties[index];
                    PropertyActions();
                }
                else Console.WriteLine($"{selection} is not a property");
            }
        }

        public string Log()
        {
            return property != null ? $"{player.Name} selected {property.Name}" : $"{player.Name} exited property selection";
        }

        public void PropertyActions()
        {
            bool exit = false;

            if(property.TryGetValue(out Property prop))
            {
                Console.WriteLine($"B for buy Houses/Hotel");
                Console.WriteLine($"S for sell Houses/Hotel");
            }

            if(!property.IsMortgaged) Console.WriteLine($"M to mortgage the property");
            else Console.WriteLine($"M to unmortgage the property");

            Console.WriteLine($"X to sell the property");
            Console.WriteLine("Waiting for input....");

            while (!exit)
            {
                var input = Console.ReadLine().ToLower();
                if (prop != null)
                {
                    if (input == "b" && player.OwnedProperties.FindAll(e => e.Color == property.Color).Count == 3)
                    {
                        exit = true;
                        CommandInvoker.Instance += new CommandBuyHouse(player, prop);
                    }

                    if (input == "s" && prop.Houses > 0)
                    {
                        exit = true;
                        CommandInvoker.Instance += new CommandSellHouse(player, prop);
                    }
                }

                if(input == "m")
                {
                    exit = true;
                    if (!property.IsMortgaged) CommandInvoker.Instance += new CommandMortgage(player, property);
                    else CommandInvoker.Instance += new CommandUnMortgage(player, property);
                }
                

                if(input == "x")
                {
                    exit = true;
                    CommandInvoker.Instance += new CommandSellProperty(player, property);
                }
            }
        }
    }
}
