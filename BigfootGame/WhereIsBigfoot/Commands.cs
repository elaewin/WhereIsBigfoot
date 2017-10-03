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

        public void Go(Player p, Location l, string direction)
        {
            // check player current location

            string currentLocation = p.PlayerLocation;

           if(l.PossibleDirections.Contains(direction))
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
