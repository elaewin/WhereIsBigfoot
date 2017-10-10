using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    public class Player
    {
        private string playerName, playerGender, playerHair;
        private Location playerLocation;
        private Dictionary<string, Item> inventory;
		private int grueCounter = 0;
		private int bigfootCounter = 0;
		private bool gameIsRunning = true;
        private List<string> grueCountdown = new List<string>()
        {
            "You think you hear something moving deeper in the cave.", 
            "You definitely hear something. A shuffling noise, that's getting louder, like something is moving towards you.", 
            "Whatever it is that moving towards you is getting closer. You think you can hear breathing. Maybe panting. And there's a smell, like rotten meat, that is getting stronger and stronger..."
        };
        private List<string> bigfootCountdown = new List<string>()
        {
            "",
            "",
            ""
        };

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

        public int GrueCounter
        {
            get => this.grueCounter;
            set => this.grueCounter = value;
        }

		public int BigFootCounter
		{
			get => this.bigfootCounter;
			set => this.bigfootCounter = value;
		}

		public bool GameIsRunning
		{
			get => this.gameIsRunning;
			set => this.gameIsRunning = value;
		}

        public List<string> GrueCountdown
        {
            get => this.grueCountdown;
            set => this.grueCountdown = value;
        }

        public List<string> BigfootCountdown
        {
            get => this.bigfootCountdown;
            set => this.bigfootCountdown = value;
        }
    }
}
