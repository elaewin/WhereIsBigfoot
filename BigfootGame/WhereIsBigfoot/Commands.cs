using System;
using System.Collections.Generic;
using System.Text;
using System.Data; 

namespace BigfootGame
{
    class Commands
    {
        // use
        // talk to
        // give

        public void Go(Player p, string direction, List<Location> locations)
        {
            Location currentLocation = p.PlayerLocation;
            string newLocation;

            if (currentLocation.Exits.ContainsKey(direction))
            {
                newLocation = currentLocation.Exits[direction];
                foreach (Location location in locations)
                {
                    if (location.LocationName == newLocation)
                    {
                        p.PlayerLocation = location;
                    }
                }
            }
            else
            {
                Console.WriteLine("You run into an impenetrable barrier and must return.");
            }

        }

        public void Get(Player p, string item)
        {
            if (p.PlayerLocation.Items.ContainsKey(item))
            {
                p.Inventory.Add(item, p.PlayerLocation.Items[item]);
                p.PlayerLocation.Items.Remove(item);
            }
            else
            {
                Console.WriteLine($"There is no {item}");
            }
        }

        public void Drop(Player p, string item)
        {
            p.PlayerLocation.Items.Add(item, p.Inventory[item]);
            p.Inventory.Remove(item);
            Console.WriteLine($"You dropped, {item}");
        }

        public void Give(Player p, string item, Character c)
        {

        }
    }
}
