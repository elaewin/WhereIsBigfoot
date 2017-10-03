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
           string newLocation; 

            if(currentLocation.Exits.ContainsKey(direction))
            {
                newLocation = currentLocation.exits[direction]; 
            }
            else
            {
            }

            // check if can go direction

            // if yes: go to location

            //if no: throw a message: not possible to go there.
        }

        public void get(string item)
        {
            Console.WriteLine("");
        }
    }
}
