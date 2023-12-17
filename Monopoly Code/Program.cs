using Monopoly.Commands;
using Monopoly.Main;

Console.WriteLine("How many rotations around the board would you like?");
string result = string.Empty;
int turns = 0;
while(turns == 0)
{
    result = Console.ReadLine();
    int.TryParse(result, out turns);
}
result= string.Empty;
Console.Clear();


Console.WriteLine("How many players? (2-4)");
int playerCount = 0;
while (playerCount < 2 || playerCount > 4 )
{
    result = Console.ReadLine();
    int.TryParse(result, out playerCount);
}
Console.Clear();


List<Player> Players = new();
for (int i = 0; i < playerCount; i++)
{
    Console.WriteLine($"Please enter player {i + 1}'s name");
    Players.Add(new Player(Console.ReadLine()));
    Console.Clear();
}


Board.Instance.Init(Players, turns);
Board.Instance.Play();

Console.Clear();
Console.WriteLine("Game Finished");

string Winner = string.Empty;
if (Board.Instance.Players.FindAll(e => e.IsBankrupted).Count != Players.Count - 1)
{
    Winner = Board.Instance.Players.MaxBy(e => e.OwnedProperties.Count).Name;
}
else Winner = Board.Instance.Players.Find(e => !e.IsBankrupted).Name;

Console.WriteLine($"{Winner} is the winner");
foreach(var player in Board.Instance.Players)
{
    Console.WriteLine();
    Console.WriteLine(player.Name);
    Console.WriteLine($"Properties: {player.OwnedProperties.Count}");
    Console.WriteLine($"Money: {player.Money}");
    string DiceRolls = "Dice rolls:";
    while(player.DiceRollHistory.Count > 0)
    {
        var roll = player.DiceRollHistory.Dequeue();

        DiceRolls += $" ({roll.Result1},{roll.Result1})";
    }
    Console.WriteLine(DiceRolls);
}


using (TextWriter tw = new StreamWriter("Logs.txt"))
{
    while (CommandInvoker.Instance.Logs.Count > 0)
    {
        var logs = CommandInvoker.Instance.Logs.Dequeue();

        tw.WriteLine(logs.Log());
    }
}


Console.WriteLine("Actions from the game will now be printed into a txt file");
Console.ReadLine();