using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Player
    {
        string playerName;
        string playerGender;
        string playerHair;
        string playerLocation;
        Dictionary<string, string> inventory;

        public Player(string playerName, string playerGender, string playerHair, string playerLocation)
        {
            this.playerName = playerName;
            this.playerGender = playerGender;
            this.playerHair = playerHair;
            this.playerLocation = playerLocation;
        }

    }
}
