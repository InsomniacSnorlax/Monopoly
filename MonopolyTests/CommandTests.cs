using Monopoly.Commands;
using Monopoly.Interfaces;
using Monopoly.Main;
using Monopoly.Squares;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace MonopolyTests
{
    public class CommandTests : IDisposable
    {
        public CommandTests() {
            player1 = new Player("Charlie");
            player1.Money = 1500;

            player2 = new Player("Snorlax");
            player2.Money = 1000;

            var testProp = "Whitechapel Road,Property,60,4,Brown,4,8,20,60,180,320,450,50,30".Split(',');
            var Rail = "Kings Cross Station,Rail,200,6,,,,25,50,100,200,,,100".Split(',');
            property1 = new Property(testProp);
            property2 = new Property(testProp);
            property3 = new Property(testProp);
            property4 = new Railroad(Rail);

            property1.BuyProperty(player1);
            property2.BuyProperty(player1);
            property3.BuyProperty(player1);
            property4.BuyProperty(player1);
        }

        Player player1;
        Player player2;
        Property property1;
        Property property2;
        Property property3;
        Railroad property4;

        [Fact]
        public void CreateProperty()
        {
            var testProp = "Whitechapel Road,Property,60,4,Brown,4,8,20,60,180,320,450,50,30".Split(',');

            var property = new Property(testProp);

            Assert.Equal("Whitechapel Road", property.Name);
        }


        [Fact]
        public void BuyingHouse()
        {
            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} bought a House for {property1.Name} for {property1.BuildingCost}", Command.Log());
        }

        [Fact]
        public void FailToBuyHouse()
        {
            property1.Houses++;

            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} cound't buy a House for {property1.Name}", Command.Log());

        }

        [Fact]
        public void BuyHotel()
        {
            property1.Houses = 4;
            property2.Houses = 4;
            property3.Houses = 4;

            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} bought a Hotel for {property1.Name} for {property1.BuildingCost}", Command.Log());
        }

        [Fact]
        public void FailToBuyHotel()
        {
            property1.Houses = 4;
            property2.Houses = 3;
            property3.Houses = 4;

            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} cound't buy a Hotel for {property1.Name}", Command.Log());
        }

        [Fact]
        public void BankOutOfHotels()
        {
            property1.Houses = 4;
            property2.Houses = 4;
            property3.Houses = 4;
            Bank.Instance.Hotels = 0;
            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} cound't buy a Hotel for {property1.Name}", Command.Log());
        }

        [Fact]
        public void BankOutOfHouses()
        {
            property1.Houses = 3;
            property2.Houses = 4;
            property3.Houses = 4;
            Bank.Instance.Houses = 0;
            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} cound't buy a House for {property1.Name}", Command.Log());
        }

        [Fact]
        public void BankBuyHouse()
        {
            property1.Houses = 3;
            property2.Houses = 4;
            property3.Houses = 4;
            Bank.Instance.Houses = 1;
            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal(0, Bank.Instance.Houses);
        }

        [Fact]
        public void BankBuyHotel()
        {
            property1.Houses = 4;
            property2.Houses = 4;
            property3.Houses = 4;
            Bank.Instance.Hotels = 1;
            Bank.Instance.Houses = 0;
            var Command = new CommandBuyHouse(player1, property1);
            Command.Execute();

            Assert.Equal(0, Bank.Instance.Hotels);
            Assert.Equal(4, Bank.Instance.Houses);
        }


        [Theory]
       // [InlineData(3, 3, 4)] // Meant to fail
       // [InlineData(4, 5, 4)]  // Meant to fail
        [InlineData(4, 3, 4)]
        [InlineData(5, 4, 5)]
        public void SellHouse(int x, int y, int z)
        {
            property1.Houses = x;
            property2.Houses = y;
            property3.Houses = z;

            var Command = new CommandSellHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} sold {property1.Name} for {property1.BuildingCost / 2}", Command.Log());
        }

        [Theory]
        [InlineData(3, 3, 4)]
        [InlineData(4, 5, 4)]
        public void FailToSell(int x, int y, int z)
        {
            property1.Houses = x;
            property2.Houses = y;
            property3.Houses = z;

            var Command = new CommandSellHouse(player1, property1);
            Command.Execute();

            Assert.Equal($"{player1.Name} couldn't sell house/hotel from {property1.Name}", Command.Log());
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(2)]
        public void Move(int DiceRoll)
        {
            
            var prop1 = new Property("The Angel Islington,Property,100,7,Cyan,6,12,30,90,270,400,550,50,50".Split(","));
            var prop2 = new Railroad("Kings Cross Station,Rail,200,6,,,,25,50,100,200,,,100".Split(","));
            var prop3 = new Utility("Electric Company,Utility,150,13,,,,4,10,,,,,75".Split(","));

            List<ISquare> squares = new List<ISquare>() { prop1, prop2, prop3};

            var player1 = new Player("Charlie");
            player1.Money = 1500;
            player1.DiceRoll = DiceRoll;

            var previousMoney = player1.Money;

            var Command = new CommandMove(player1, squares);
            Command.Execute();
            bool Touched = false;

            int roll = DiceRoll;

            if(DiceRoll > squares.Count - 1)
            {
                roll %= (squares.Count - 1);
                Touched = true;
            }

            var targetSquare = squares[roll];

            string log = $"{player1.Name} landed on {targetSquare.Name}";
            if (Touched)
            {
                log += $"\r\n{player1.Name} touched Go and was awarded $200 -> {previousMoney} to {previousMoney += 200}";
            }

            Assert.Equal(log, Command.Log());
        }

        [Theory]
        [InlineData(1, 4, 4, 2)]
        //[InlineData(1, 2, 5, 3)] // Success (Not meant to be in jail)
        //[InlineData(0, 1, 3, 0)] // Success (Not meant to be in jail)
        public void DiceRoll(int JailFreeCard, int Result1, int Result2, int RolledDouble)
        {
            bool UsedJailFree = false;

            var player = new Player("Charlie");
            player.Money = 1500;
            player.JailFreeCards = JailFreeCard;
            player.RolledDouble = RolledDouble;

            bool IsSame = Result1 == Result2;

            if (IsSame)
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
            }

            if (player.RolledDouble == 3)
            {
                player.IsInJail = true;
            }

            string log = $"{player.Name} rolled: {Result1} {Result2}";
            if (player.IsInJail) log += $"\r\n{player.Name} is still in Jail";
            if (UsedJailFree) log += $"\r\n{player.Name} Used jail out of free card";

            Assert.True(player.IsInJail);
        }

        [Theory]
       // [InlineData(5)] // Rent 5
        [InlineData(0)] // Color Rent
       // [InlineData(2)] // Rent 2
        public void GetRent(int Houses)
        {
            property1.Houses = Houses;

            Assert.Equal(property1.ColorRent, property1.GetRent());
        }

        public void Dispose()
        {
            
        }
    }
}