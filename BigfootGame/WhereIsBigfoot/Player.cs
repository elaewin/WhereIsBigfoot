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
            "\nToo late! Bigfoot's almost here, and you’re out of ideas.\n\nAnd when you’re out of ideas, there’s only one idea left: RUN!\n\nBigfoot is practically on top of you, and y’know, there’s those big feet to contend with, but you run. You run as fast as terror and regret for going in on Dan’s ideas can take you. Heck, you run so fast, you even lose your backpack. Who cares? It’s slowing you down anyway!\n\nYou look back, though, just once to see if Bigfoot is still chasing you. And it turns out looking backward while in the middle of Run For Your Life Speed is a little disorienting. You don’t see Bigfoot right away...but when you look back around, you definitely see the big tree trunk.\n\nAlthough you don’t quite see it in ti—\n\n\n\nYou awaken on the ground. Your nose hurts. It really hurts. The world is darker now, but as it comes into focus you realize that might be more about the clouds gathering overhead. However, you’re alive, and right where you left off, so that’s good.\n\nCrawling to your feet, you look around. There’s no Bigfoot anymore. Just lots of big footprints, and your backpack, torn open with its contents strewn around. Fortunately, just about everything is still here, laying in plain sight.\n\nAh well. You’re alive. You saw Bigfoot. And you’ve got a story to tell, and lots of photos to...sigh. No, no photos. Your phone is still dead. Also, those clouds look like they’re full of rain. Thankfully, you see the lantern nearby, and it still has enough oil to get you back to camp. Hopefully you’ll get there before the rain starts. But it’s almost certainly going to wash away these tracks.\n\nAs you gather your things and head out, you wonder if maybe this is why nobody ever produces proof of Bigfoot. It’s not as easy as it sounds.\n\n*** GAME OVER ***"
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
