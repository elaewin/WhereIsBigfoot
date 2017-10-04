using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Player
    {
        string playerName, playerGender, playerHair, inventory;
        Location playerLocation;
        //Dictionary<string, string> inventory;

        public Player(string playerName, string playerGender, string playerHair, Location playerLocation, string inventory)
        {
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerHair = playerHair;
            this.playerLocation = playerLocation;
            this.inventory = inventory;
        }

        public string PlayerName
        {
            get { return this.playerName; }
        }

        public string PlayerGender
        {
            get { return this.playerGender; }
        }

        public string PlayerHair
        {
            get { return this.playerHair; }
        }

        public Location PlayerLocation
        {
            get { return this.playerLocation; }
        }

        //public Dictionary<string, string> Inventory
        //{
        //    get { return this.inventory; }
        //    set { this.inventory = value; }
        //}
    }
}
