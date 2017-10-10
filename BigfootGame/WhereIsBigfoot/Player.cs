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
            "\nYou think you hear something moving deeper in the cave.", 
            "\nYou definitely hear something. A shuffling noise, that's getting louder, like something is moving towards you.", 
            "\nWhatever it is that moving towards you is getting closer. You think you can hear breathing. Maybe panting. And there's a smell, like rotten meat, that is getting stronger and stronger..."
        };
        private List<string> bigfootCountdown = new List<string>()
        {
            "\nThe valley is pretty wide, and Bigfoot is pretty far away, but it turns out that with those big feet, he can REALLY move. He'll be on you soon!",
            "\nNot to to alarm you, but Bigfoot is closer than ever, and he still looks pretty pissed off. You figure you've got time to do one, or maybe two more things before he reaches you.",
            "\nYou have only seconds left to figure out something brilliant to do to keep Bigfoot from carrying out the imminent violence you can see in his eyes! Things are going to get really unpleasant for you otherwise...",
            "\n"
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
