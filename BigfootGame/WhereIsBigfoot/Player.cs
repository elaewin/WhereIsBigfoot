﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    public class Player
    {
        string playerName, playerGender, playerHair;
        Location playerLocation;
        Dictionary<string, Item> inventory;
        // add counter 

        public Player(string playerName, string playerGender, string playerHair)
        {
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerHair = playerHair;
            this.inventory = new Dictionary<string, Item>();
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
            set { this.playerLocation = value; }
        }

        public Dictionary<string, Item> Inventory 
        { 
            get { return this.inventory; }
            set { this.inventory = value; }
        }
    }
}
