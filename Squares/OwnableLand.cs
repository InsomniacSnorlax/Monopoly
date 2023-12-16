﻿using Monopoly.Commands;
using Monopoly.Enums;
using Monopoly.Interfaces;

namespace Monopoly.Squares
{
    public abstract class OwnableLand : ISquare
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Color { get; set; }
        public int Rent { get; set; }
        public int Mortgage { get; set; }
        public bool IsMortgaged { get; set; }
        public Player Owner { get; set; } = null;
        public virtual SquareType Type { get; set; }
        public int Position { get; set; }

        public void BuyProperty(Player player)
        {
            Owner = player;
            player.OwnedProperties.Add(this);
            player.Money -= Cost;
        }

        public void SellProperty()
        {
            Owner.Money += Cost / 2;
            Owner.OwnedProperties.Remove(this);
            Owner = null;
        }

        public int Mortgaged()
        {
            IsMortgaged = true;

            return Mortgage;
        }

        public int UnMortgage()
        {
            IsMortgaged = false;

            return (int)(Mortgage * 1.1f);
        }

        public virtual void Landed()
        {
            Player currentPlayer = Board.Instance.currentPlayer;

            if (Owner != null && currentPlayer != Owner && !IsMortgaged)
            {
                CommandInvoker.Instance += new CommandRent(this, currentPlayer);
            }
        }

        public virtual void PayRent(Player currentPlayer, int rent)
        {
            currentPlayer.Money -= rent;
            Owner.Money += rent;
        }


        public virtual int GetRent()
        {
            return 0;
        }
    }
}
