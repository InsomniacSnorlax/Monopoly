using Monopoly.Interfaces;
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
            Console.WriteLine("Type Esc to return to menu");
            for (int i = 0; i < player.OwnedProperties.Count; i++)
            {
                Console.WriteLine($"{i}     {player.OwnedProperties[i].Name}");
            }

            var selection = Console.ReadLine();
            if (selection.ToLower() == "esc") return;
            if (int.TryParse(selection, out int index))
            {
                property = player.OwnedProperties[index];
            }
            else Console.WriteLine($"{selection} is not a property");
        }

        public string Log()
        {
            return property != null ? $"{player.Name} selected {property.Name}" : $"{player.Name} exited property selection";
        }

        public void PropertyActions()
        {

        }
    }
}
