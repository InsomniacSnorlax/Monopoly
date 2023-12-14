
// See https://aka.ms/new-console-template for more information
using Monopoly;

Console.WriteLine("Hello, World!");
var board = new Board();

board.test.ForEach(x => x.Landed(board));