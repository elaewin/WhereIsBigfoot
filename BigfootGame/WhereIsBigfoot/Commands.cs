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
                        if (itemToUse.Name == "book" | i.Target == target)
                        {
                            TypeLine(wrapText(itemToUse.Actions["use"]));
                        }
                        else
                        {
                            CannotVerbNoun("use", item);
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
            TypeLine(wrapText(itemToTransfer.Actions["get"]));
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
					TypeLine(wrapText(c.Actions["talk"]));
				}
				else
				{
					TypeLine("This character does not exist in this location.");
				}
			}
		}

        public void Put(Player p, string name, List<Item> items)
        {
            TypeLine("What are you trying to put? you need 2 items.");

            TypeLine("Type the first item:");
            string item1 = Console.ReadLine();
            Console.WriteLine();
            TypeLine("Type the second item:");
            string item2 = Console.ReadLine();
            Console.WriteLine();
            List<string> tools = new List<string>();
            tools.Add(item1);
            tools.Add(item2);

            if (tools.Contains("lantern") && tools.Contains("grease"))
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

            else
            {
                TypeLine("You can only use \"put\" to fill your lantern with the bacon grease.");
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
                TypeLine($"{item.DescriptionShort} \n\n");
            }

        }

        public void Look(Player p, string entry)
        {
            foreach (Item item in p.Inventory.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    TypeLine(wrapText($"{item.DescriptionLong} \n"));
                    return;
                }
            }
            foreach (Item item in p.PlayerLocation.Items.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    TypeLine(wrapText($"{item.DescriptionLong} \n"));
                    return;
                }
            }
            foreach (Character character in p.PlayerLocation.Characters.Values)
            {
                if (character.ParseValue.Contains(entry))
                {
                    TypeLine(wrapText($"{character.DescriptionLong} \n"));
                    return;
                }
            }
            if (entry == "none")
            {
                TypeLine(wrapText($"{p.PlayerLocation.DescriptionLong} \n"));
                return;
            }
            TypeLine($"I don't see {entry} here. \n");
        }

        public void ShowLocation(Location location)
        {
            if (location.Visited == false)
            {
                TypeLine(wrapText($"{location.DescriptionFirst}"));

                foreach (Character character in location.Characters.Values)
                    TypeLine(wrapText($"{character.DescriptionFirst}"));

                foreach (Item item in location.Items.Values)
                    TypeLine(wrapText($"{item.DescriptionFirst}"));

                location.Visited = true;
            }
            else
            {
                TypeLine(wrapText($"{location.DescriptionShort}"));

                foreach (Character character in location.Characters.Values)
                    Console.WriteLine(wrapText($"{character.DescriptionShort}"));

                foreach (Item item in location.Items.Values)
                    TypeLine(wrapText($"{item.DescriptionShort}"));
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

        //public string wrapText(string paragraph)
        //{
        //    if (string.IsNullOrWhiteSpace(paragraph))
        //    {
        //        return string.Empty;
        //    }

        //    var approxLineCount = paragraph.Length / Console.WindowWidth;
        //    var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

        //    for (var i = 0; i < paragraph.Length;)
        //    {
        //        var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
        //        var line = paragraph.Substring(i, grabLimit);

        //        var isLastChunk = grabLimit + i == paragraph.Length;

        //        if (isLastChunk)
        //        {
        //            i = i + grabLimit;
        //            lines.Append(line);
        //        }
        //        else
        //        {
        //            var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
        //            lines.AppendLine(line.Substring(0, lastSpace));

        //            //Trailing spaces needn't be displayed as the first character on the new line
        //            i = i + lastSpace + 1;
        //        }
        //    }
        //    return lines.ToString();

        //}


        public string wrapText(String text)
        {
            String[] words = text.Split(' ');
            StringBuilder buffer = new StringBuilder();

            foreach (String word in words)
            {
                buffer.Append(word);
                //see if you can make this dynamic.
                if (buffer.Length >= 80)
                {
                    String line = buffer.ToString().Substring(0, buffer.Length - word.Length);
                    Console.WriteLine(line);
                    buffer.Clear();
                    buffer.Append(word);
                }

                buffer.Append(" ");

            }
            //buffer.ToString().PadLeft(200);
            //buffer.ToString().PadRight(200);
            //Console.WriteLine(buffer.ToString());
            return buffer.ToString();
        }


    }
}


