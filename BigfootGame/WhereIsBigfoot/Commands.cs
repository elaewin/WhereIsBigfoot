using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
    class Commands
    {
        // talk to
        // put - two items interacting with each other
        // help 

        // TODO: check if action is available 

        public void Use(Player p, string item)
        {
            if (p.Inventory.ContainsKey(item))
            {
                Item itemToUse = p.Inventory[item];
                Console.WriteLine(itemToUse.Actions["use"]);
            }
            else
            {
                Console.WriteLine($"You do not have {item} in your inventory. Please acquire {item}");
            }
        }

        public void Go(Player p, string direction, List<Location> locations)
        {
            Location currentLocation = p.PlayerLocation;
            string newLocation;

            if (currentLocation.Exits.ContainsKey(direction))
            {
                newLocation = currentLocation.Exits[direction];
                foreach (Location location in locations)
                {
                    if (location.Name == newLocation)
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

        // fix Get to be able to take item and character 
        public void Get(Player p, string item)
        {
            if (p.PlayerLocation.Items.ContainsKey(item))
            {
                TransferItem(p, item);
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

        public void Give(Player p, string item, List<Character> characters)
        {
            DanCheck(p, item, characters);
            p.Inventory.Remove(item);
        }

        private void TransferItem(Player p, string item)
        {
            Item itemToTransfer = p.PlayerLocation.Items[item];
            p.Inventory.Add(item, itemToTransfer);
            p.PlayerLocation.Items.Remove(item);
            Console.WriteLine(itemToTransfer.Actions["get"]);
        }

        private void DanCheck(Player p, string item, List<Character> characters)
        {
            if (p.PlayerLocation.Name == "dan")
            {
                if (item == "book" && p.PlayerLocation.Characters.ContainsKey("danCooking"))
                {
                    TransferItem(p, item);
                    foreach (Character c in characters)
                    {
                        if (c.Name == "danReading")
                        {
                            p.PlayerLocation.Characters.Add(c.Name, c);
                            p.PlayerLocation.Characters["danReading"].Location = p.PlayerLocation.Name;
                            p.PlayerLocation.Characters.Remove("danCooking");
                        }
                    }
                }
            }
        }

        public void Help(Player p)
        {
            Console.WriteLine($"Hey {p.PlayerHair} hair, I dont freaking understand that! Use a 2 word command format: ");
            Console.WriteLine($"ie. get item -or- go north");
            Console.WriteLine($"Possible commands for {p.PlayerName}: get, go, give, use, talk, put, help, quit, inventory");
        }

        public void Inventory(Player p)
        {
            Console.Write("You have the following inventory: ");
            foreach (var item in p.Inventory.Values)
            {
                Console.Write($"{item.DescriptionShort} ");
            }
        }
    }

}
