using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
    public class Commands
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
                Console.WriteLine($"You do not have {item} in your inventory. You'll have to try something else. \n");
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
						ShowLocation(location);
                    }
                }
            }
            else
            {
                Console.WriteLine("You run into an impenetrable barrier and must return. \n");
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
                Console.WriteLine($"There is no {item} \n");
            }
        }

        public void Drop(Player p, string item)
        {
            p.PlayerLocation.Items.Add(item, p.Inventory[item]);
            p.Inventory.Remove(item);
            Console.WriteLine($"You dropped {item} \n");
        }

        public void Give(Player p, string item, Dictionary<string, Character> characters)
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

        private void DanCheck(Player p, string item, Dictionary<string, Character> characters)
        {
            if (p.PlayerLocation.Name == "danCamp")
            {
                if (item == "book" && p.PlayerLocation.Characters.ContainsKey("danCooking"))
                {
                    TransferItem(p, item);
                    foreach (Character c in characters.Values)
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
            Console.WriteLine("You have the following inventory: \n");
            foreach (var item in p.Inventory.Values)
            {
                Console.WriteLine($"{item.DescriptionShort} \n\n");
            }

        }

        public void Look(Player p, string entry)
        {
            foreach (Item item in p.Inventory.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    Console.WriteLine($"{item.DescriptionLong} \n");
					return;
                }
            }
            foreach (Item item in p.PlayerLocation.Items.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    Console.WriteLine($"{item.DescriptionLong} \n");
					return;
                }
            }
            foreach (Character character in p.PlayerLocation.Characters.Values)
            {
                if (character.ParseValue.Contains(entry))
                {
                    Console.WriteLine($"{character.DescriptionLong} \n");
					return;
                }
            }
			if (entry == "none")
			{
				Console.WriteLine($"{p.PlayerLocation.DescriptionLong} \n");
				return;
			}
			Console.WriteLine($"I don't see {entry} here. \n");
        }

		public void ShowLocation(Location location)
		{
			if (location.Visited == false)
			{
				Console.WriteLine($"{location.DescriptionFirst}");

				foreach (Character character in location.Characters.Values)
					Console.WriteLine($"{character.DescriptionFirst}");

				foreach (Item item in location.Items.Values)
					Console.WriteLine($"{item.DescriptionFirst}");

				location.Visited = true;
			}
			else
			{
				Console.WriteLine($"{location.DescriptionShort}");

				foreach (Character character in location.Characters.Values)
					Console.WriteLine($"{character.DescriptionShort}");

				foreach (Item item in location.Items.Values)
					Console.WriteLine($"{item.DescriptionShort}");
			}


		}
		}
    }

}
