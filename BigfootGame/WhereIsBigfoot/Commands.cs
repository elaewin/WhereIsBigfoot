using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 

namespace BigfootGame
{
    class Commands
    {
        // get
        // drop
        // put
        // use
        // talk to
        // give
        // go 

        public string Go(Player p, string direction)
        {
            // check player current location

           Location currentLocation = p.PlayerLocation;
           string newLocation = ""; 

            if(currentLocation.Exits.ContainsKey(direction))
            {
                newLocation = currentLocation.Exits[direction]; 
            }
            else
            {
                Console.WriteLine("You run into an impenetrable barrier and must return.");
            }

            return newLocation;
        }

        public void GetItem(Player p, string item)
        {
            
        }
    }
}
