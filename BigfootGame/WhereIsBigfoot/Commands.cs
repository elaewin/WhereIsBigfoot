using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
	public class Commands
	{
		public void Use(Player p, string item, string target)
		{
			Location location = p.PlayerLocation;
			foreach (Item itemToUse in p.Inventory.Values)
			{
				if (itemToUse.ParseValue.Contains(item))
				{

					foreach (Item i in location.Items.Values)
					{
						if (i.ParseValue.Contains(target))
						{
							TypeLine($"That {item} does not exist here");
						}
						else
						{
							TypeLine(itemToUse.Actions["use"]);
						}
					}
					foreach (Character c in location.Characters.Values)
					{
						if (c.ParseValue.Contains(target))
						{
							Console.WriteLine(itemToUse.Actions["use"]);
						}
						else
						{
							TypeLine(itemToUse.Actions["use"]);
						}
					}
				}
			}
		}


		public void Go(Player p, string direction, List<Location> locations)
		{
			Location currentLocation = p.PlayerLocation;
			string newLocation;

			if (currentLocation.Exits.ContainsKey(direction))
			{
				Console.Title = Console.Title.Remove(16);
				newLocation = currentLocation.Exits[direction];
				foreach (Location location in locations)
				{
					if (location.Name == newLocation)
					{
						p.PlayerLocation = location;
						Console.Title += $" {location.DescriptionShort}";
						Console.WriteLine();
						ShowLocation(location);

					}
				}
			}
			else
			{
				CannotVerbNoun("go", direction);
			}

		}

		public void Get(Player p, string name)
        {
            if (p.PlayerLocation.Items.ContainsKey(name))
            {
                if (p.PlayerLocation.Items[name].Actions.ContainsKey("get"))
                {
                    TransferItem(p, name);
                }
                else
                {
                    CannotVerbNoun("get", name);
                }
            }
            else if (p.PlayerLocation.Characters.ContainsKey(name))
            {
                if (p.PlayerLocation.Characters[name].Actions.ContainsKey("get"))
                {
                    TypeLine(p.PlayerLocation.Characters[name].Actions["get"]);
                }
                else
                {
                    CannotVerbNoun("get", name);
                }
            }
            else
            {
                CannotVerbNoun("get", name);
            }
        }

		public void Drop(Player p, string item)
		{
			p.PlayerLocation.Items.Add(item, p.Inventory[item]);
			p.Inventory.Remove(item);
			TypeLine($"You dropped {item} \n");
		}

		// check from to 
		public void Give(Player p, string item, Dictionary<string, Character> characters, string target)
		{
			DanCheck(p, item, characters);
			p.Inventory.Remove(item);
		}


		private void TransferItem(Player p, string item)
		{
			Item itemToTransfer = p.PlayerLocation.Items[item];
			p.Inventory.Add(item, itemToTransfer);
			p.PlayerLocation.Items.Remove(item);
			TypeLine(itemToTransfer.Actions["get"]);
		}

		private void DanCheck(Player p, string item, Dictionary<string, Character> characters)
		{
			if (p.PlayerLocation.Name == "danCamp")
			{
				if (item == "book" && p.PlayerLocation.Characters.ContainsKey("danCooking"))
				{
					p.Inventory.Remove(item);
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
		public void Talk(Player p, String name, Dictionary<string, Character> characters, string target)
		{
			foreach (Character c in characters.Values)
			{
				if (p.PlayerLocation.Characters.ContainsKey(name))
				{
					TypeLine(c.Actions["talk"]);
				}
				else
				{
					TypeLine("This character does not exist in this location.");
				}
			}
		}

		public void Put(Player p, string name, List<Item> items)
		{
			if (p.Inventory.ContainsKey("lantern") && p.Inventory.ContainsKey("grease"))
			{
				TypeLine("Now your lantern is full and you can use it to go in the cave.");
				p.Inventory.Remove("lantern");
				foreach (Item i in items)
				{
					if (i.Name == "filledLantern")
						p.Inventory.Add("filledLantern", i);
				}
			}
			else
			{
				TypeLine("You need to have the lantern and the grease in your inventory before you can use it.");
			}
		}

		public void Help(Player p)
		{
			TypeLine($"Hey {p.PlayerHair} hair, I dont freaking understand that! Use a 2 word command format: ");
			TypeLine($"ie. get item -or- go north");
			TypeLine($"Possible commands for {p.PlayerName}: get, go, give, use, talk, put, help, quit, inventory");
		}

		public void Inventory(Player p)
		{
			TypeLine("You have the following inventory: \n");
			foreach (var item in p.Inventory.Values)
			{
				TypeLine($"{item.DescriptionShort}");
			}

		}

		public void Look(Player p, string entry)
		{
			foreach (Item item in p.Inventory.Values)
			{
				if (item.ParseValue.Contains(entry))
				{
					TypeLine($"{item.DescriptionLong} \n");
					return;
				}
			}
			foreach (Item item in p.PlayerLocation.Items.Values)
			{
				if (item.ParseValue.Contains(entry))
				{
					TypeLine($"{item.DescriptionLong} \n");
					return;
				}
			}
			foreach (Character character in p.PlayerLocation.Characters.Values)
			{
				if (character.ParseValue.Contains(entry))
				{
					TypeLine($"{character.DescriptionLong} \n");
					return;
				}
			}
			if (entry == "none")
			{
				TypeLine($"{p.PlayerLocation.DescriptionLong} \n");
				return;
			}
			TypeLine($"I don't see {entry} here. \n");
		}

		public void ShowLocation(Location location)
		{
			if (location.Visited == false)
			{
				TypeLine($"{location.DescriptionFirst}");

				foreach (Character character in location.Characters.Values)
					TypeLine($"{character.DescriptionFirst}");

				foreach (Item item in location.Items.Values)
					TypeLine($"{item.DescriptionFirst}");

				location.Visited = true;
			}
			else
			{
				TypeLine($"{location.DescriptionShort}");

				foreach (Character character in location.Characters.Values)
					Console.WriteLine($"{character.DescriptionShort}");

				foreach (Item item in location.Items.Values)
					TypeLine($"{item.DescriptionShort}");
			}


		}

		private void CannotVerbNoun(string verb, string noun)
		{
			TypeLine($"You can't {verb} {noun} \n");
		}

		public void TypeLine(string line)
		{
			for (int i = 0; i < line.Length; i++)
			{
				Console.Write(line[i]);
				System.Threading.Thread.Sleep(15); // Sleep for 15 milliseconds between characters.
			}
			Console.WriteLine();
		}

	}
}


