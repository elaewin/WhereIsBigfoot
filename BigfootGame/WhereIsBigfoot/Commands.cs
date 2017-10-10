using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;

namespace WhereIsBigfoot
{
    // commands are in alpha order
    // helper functions at bottom 

    // TODO: null check for all 

    public class Commands
    {
        private int textLoadSpeed;
        public int userSpeed;
        // Used in TypeLine. Sleep for 15 milliseconds between characters.
        // no checks needed 

        // DONE
        // passing player, asset 
        // TESTME: takes new player, takes an asset, test "RESULT"
        public void Drop(Player p, Asset a)
        {
            // check if passed null 
            if (a != null)
            {
                Item item = (Item)a;
                p.PlayerLocation.Items.Add(item.Name, item);
                p.Inventory.Remove(item.Name);
                // RESULT
                TypeLine(item.Actions["drop"]);
            }
            else
            {
                // handle item doesn't exist
                // RESULT
                TypeLine($"You do not have {a.Name} in your inventory.");
            }
        }

        // DONE
        // TESTME: accepts player, item, dictionary
        public void Get(Player p, Item item, List<Item> items)
        {
            // if asset is an item
            if (p.PlayerLocation.Items.ContainsKey(item.Name))
            {
                if (p.PlayerLocation.Items[item.Name].Actions.ContainsKey("get"))
                {
                    if (item.Name == "grease")
                    {
                        DanCheck(p, item);
                    }
                    else if (item.Name == "blackberries")
                    {
                        BlackberryCheck(p, item, items);
                    }
                    else
                    {
                        TransferItem(p, item);
                    }
                }
            }
            // if asset is a character
            // go back to later
            //else if (p.PlayerLocation.Characters.ContainsKey(a.Name))
            //{
            //    Character character = (Character)a;
            //    if (p.PlayerLocation.Characters[character.Name].Actions.ContainsKey("get"))
            //    {
            //        TypeLine(p.PlayerLocation.Characters[character.Name].Actions["get"]);
            //    }
            //    else
            //    {
            //        CannotVerbNoun("get", a.Name);
            //        TypeLine($"Getting {a.Name} would be rude and they do not fit in your backpack.");
            //    }
            //}
            // 
            else
            {
                CannotVerbNoun("get", item.Name);
                TypeLine("Let's face it, you just have to let go and move on.");
            }
        }

        // DONE takes player, item, two characters
        // see RESULT in SwitchChar 
        // player, item as an item, character as a character
        // character dictionary passed 
        public void Give(Player p, Item item, Character character, Dictionary<string, Character> characters)
        {
            // check to see if character is target
            if (item.Target == "danCooking" || item.Target == "bigfootHostile")
            {
                // give Dan book
                if (item.Name == "book" && character.Name == "danCooking")
                {
                    SwitchChar(p, item, character, characters, "danReading");
                }
                // RESULT check if bigfootHostile is removed from characters dict in player location
                // RESULT check if bigfootFriendly is in characters dict in player location
                else if (item.Name == "canOfBerries" && character.Name == "bigfootHostile")
                {
                    SwitchChar(p, item, character, characters, "bigfootFriendly");
                }
            }
            // RESULT
            // handle mismatch
            else
            {
                CannotVerbNoun("give", item.Name);
                TypeLine($"Maybe {character} doesn't want the {item.Name}.");
            }
        }

        // Handle tunnel = checking location 
        // check inventory and check if lantern is lit 
        // tunnel 1 or tunnel 4 (check against map) 
        // - lantern is lit - different tunnel
        // - tunnel1Lit (naming convention) 
        // - lantern is dark - has three moves 
        // - if leave counter reset 
        // - special case, handle tunnel 
        // - handle counter for player 
        // Handle walking stick = checking inventory 
        // If in mountain and try to go to the cave
        // - without walking stick 
        public void Go(Player p, string direction, List<Location> locations)
        {
            Location currentLocation = p.PlayerLocation;
            string newLocation;
            if (currentLocation.Exits.ContainsKey(direction))
            {
                Console.Title = Console.Title.Remove(16);
                newLocation = currentLocation.Exits[direction];
                if (currentLocation.Name == "woods5" && direction == "north")
                {
                    Mountain(p, locations, currentLocation);
                }
                else if (currentLocation.Name == "mountain")
                {
                    Tunnel(p, locations);
                }
                else
                {
                    foreach (Location location in locations)
                    {
                        if (location.Name == newLocation)
                        {
                            GoToLocation(p, location);
                        }
                    }
                }
            }
            else
            {
                CannotVerbNoun("go", direction);
                WrapText("Try a different direction. Up is also an option.");
            }
        }

        // DONE
        public void Help(Player p, List<string> allowedVerbs)
        {
			WrapText("\nYou pull out your Bigfoot Sighting Assistance Manual! It reads:\n\nTrying to figure out where you are? Your current location is displayed in the title bar at the top of your the game's console window. Also, entering the command 'Look' in any location will give you a description of that location.\n\nNavigation through the forest of Where Is Bigfoot uses two word commands in the format VERB NOUN. For instance, 'Get Book', or 'Climb Tree'.\n\nTo navigate between different locations in the game, use the verb 'Go', followed by the direction you wish to travel. Many other verbs that describe travel may also be used. We'll let you figure out which ones...\n\n");

            WrapText($"Some possible verbs for {p.PlayerName} are: \n");
            foreach (string verb in allowedVerbs)
            {
                Console.WriteLine($"    {verb}");
            }
        }

        // DONE
        public void Inventory(Player p)
        {
            TypeLine("You have the following items in your inventory: ");
            foreach (var item in p.Inventory.Values)
            {
                TypeLine($"{item.Title}");
            }
        }

        // DONE
        public void Look(Player p, string entry)
        {
            foreach (Item item in p.Inventory.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    WrapText($"{item.DescriptionLong} \n");
                    return;
                }
            }
            foreach (Item item in p.PlayerLocation.Items.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    WrapText($"{item.DescriptionLong} \n");
                    return;
                }
            }
            foreach (Character character in p.PlayerLocation.Characters.Values)
            {
                if (character.ParseValue.Contains(entry))
                {
                    WrapText($"{character.DescriptionLong} \n");
                    return;
                }
            }

            if (entry == "none")
            {
                WrapText($"{p.PlayerLocation.DescriptionLong}\n");
                string descriptions = "";
                foreach (Character character in p.PlayerLocation.Characters.Values)
                    descriptions += character.DescriptionShort;
                foreach (Item item in p.PlayerLocation.Items.Values)
                    descriptions += item.DescriptionShort;

                if (descriptions != "")
                    WrapText($"{descriptions}");

                WrapText($"{p.PlayerLocation.Exits["text"]}");
                return;
            }
        }

        // DONE
        // write like use 
        // DONE        
        public void Put(Player p, Item item, Asset asset, List<Item> items)
        {
            if (item.Name == "lantern" & asset.Name == "grease")
            {
                Lantern(p, item, asset, items, "filledLantern");
            }
            else if (item.Name == "filledLantern" & asset.Name == "matches")
            {
                Lantern(p, item, asset, items, "glowingLantern");
            }
            else if (item.Target == asset.Name)
            {
                WrapText(item.Actions["put"]);
            }
            else
            {
                WrapText($"You can't put {item.Name} in {asset.Name}.");
                WrapText($"Are you sure you're using {item.Name} correctly?");
            }
        }

        // DONE
        public void Quit(Player p)
        {
            p.GameIsRunning = false;
            Console.WriteLine();
            WrapText("Thank you for playing Where is Bigfoot!");
            Console.WriteLine();
        }

        // DONE
        public void Talk(Player p, Character c)
        {
            WrapText(c.Actions["talk"]);
        }

        // DONE
        // player, item, asset 
        // check if asset is target
        public void Use(Player p, Item item, Asset asset)
        {
            if (item.Name == "book" | item.Target == asset.Name)
            {
                WrapText(item.Actions["use"]);
            }
            else
            {
                WrapText($"You can't use {item.Name} on {asset.Name}.");
                WrapText($"Are you using {item.Name} correctly?");
            }
        }

        public void ShowLocation(Location location)
        {
            string descriptions = "";

            if (location.Visited == false)
            {
                WrapText($"{location.DescriptionFirst}\n");
                foreach (Character character in location.Characters.Values)
                    descriptions += character.DescriptionFirst;
                foreach (Item item in location.Items.Values)
                    descriptions += item.DescriptionFirst;

                if (descriptions != "")
                    WrapText($"{descriptions}");

                location.Visited = true;
            }
            else
            {
                WrapText($"{location.DescriptionShort}\n");
                foreach (Character character in location.Characters.Values)
                    descriptions += character.DescriptionShort;
                foreach (Item item in location.Items.Values)
                    descriptions += item.DescriptionShort;

                if (descriptions != "")
                    WrapText($"{descriptions}");
            }
            WrapText($"{location.Exits["text"]}");
        }

        // >>> AUXILIARY METHODS <<< 

        private void Lantern(Player p, Item item, Asset asset, List<Item> items, string newState)
        {
            Item target = (Item)asset;
            foreach (Item lantern in items)
            {
                if (lantern.Name == newState)
                {
                    WrapText(target.Actions["put"]);
                    p.Inventory.Add(newState, lantern);
                    p.Inventory.Remove(item.Name);
                    p.Inventory.Remove(target.Name);
                }
            }
        }

		// Method adapted from https://rianjs.net/2016/03/line-wrapping-at-word-boundaries-for-console-applications-in-csharp
		public void WrapText(string paragraph)
        {
            if (string.IsNullOrWhiteSpace(paragraph))
            {
                return;
            }

            var approxLineCount = paragraph.Length / Console.WindowWidth;
            var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

            for (var i = 0; i < paragraph.Length;)
            {
                var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
                var line = paragraph.Substring(i, grabLimit);

                var isLastChunk = grabLimit + i == paragraph.Length;

                if (isLastChunk)
                {
                    i = i + grabLimit;
                    lines.Append(line);
                }
                else
                {
                    var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
                    lines.AppendLine(line.Substring(0, lastSpace));

                    //Trailing spaces needn't be displayed as the first character on the new line
                    i = i + lastSpace + 1;
                }
            }
            TypeLine(lines.ToString());
        }

        private void GoToLocation(Player p, Location location)
        {
            p.PlayerLocation = location;
            Console.Title += $"? -- {location.Title}";
            Console.WriteLine();
            ShowLocation(location);
        }

        // tunnel 1 or tunnel 5 (check against map) 
        // - lantern is dark - has three moves 
        // - if leave counter reset 
        // - special case, handle tunnel 
        // - handle counter for player 
        private void Tunnel(Player p, List<Location> locations)
        {
            if (p.Inventory.ContainsKey("glowingLantern"))
            {
                foreach (Location location in locations)
                {
                    switch (location.Name)
                    {
                        case "tunnel1Lit":
                            if (p.PlayerLocation.Name == "mountain")
                            {
                                GoToLocation(p, location);
                            }
                            break;
                        case "tunnel2Lit":
                            if (p.PlayerLocation.Name == "tunnel1Lit")
                            {
                                GoToLocation(p, location);
                            }
                            break;
                        case "tunnel3Lit":
                            if (p.PlayerLocation.Name == "tunnel2Lit")
                            {
                                GoToLocation(p, location);
                            }
                            break;
                        case "tunnel4Lit":
                            if (p.PlayerLocation.Name == "tunnel3Lit")
                            {
                                GoToLocation(p, location);
                            }
                            break;
                        case "tunnel5Lit":
                            if (p.PlayerLocation.Name == "tunnel4Lit")
                            {
                                GoToLocation(p, location);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                foreach (Location location in locations)
                {
                    switch (location.Name)
                    {
                        case "tunnel1":
                            if (p.GrueCounter < 3 && p.PlayerLocation.Name == "mountain")
                            {
                                GoToLocation(p, location);
                                WrapText(p.GrueCountdown[p.GrueCounter]);
                            }
                            else
                            {

                            }
                            break;
                        case "tunnel2":
                            if (p.GrueCounter < 3 && p.PlayerLocation.Name == "tunnel1")
                            {
                                GoToLocation(p, location);
                                WrapText(p.GrueCountdown[p.GrueCounter]);
                                p.GrueCounter++;
                            }
                            else
                            {

                            }
                            break;
                        case "tunnel3":
                            if (p.GrueCounter < 3 && p.PlayerLocation.Name == "tunnel2")
                            {
                                GoToLocation(p, location);
                                WrapText(p.GrueCountdown[p.GrueCounter]);
                                p.GrueCounter++;
                            }
                            else
                            {

                            }
                            break;
                        case "tunnel4":
                            if (p.GrueCounter < 3 && p.PlayerLocation.Name == "tunnel3")
                            {
                                GoToLocation(p, location);
                                WrapText(p.GrueCountdown[p.GrueCounter]);
                                p.GrueCounter++;
                            }
                            else
                            {

                            }
                            break;
                        case "tunnel5":
                            if (p.GrueCounter < 3 && p.PlayerLocation.Name == "tunnel3")
                            {
                                GoToLocation(p, location);
                                WrapText(p.GrueCountdown[p.GrueCounter]);
                                p.GrueCounter++;
                            }
                            else
                            {

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void Mountain(Player p, List<Location> locations, Location currentLocation)
        {
            if (p.Inventory.ContainsKey("stick"))
            {
                foreach (Location location in locations)
                {
                    if (location.Name == "mountain")
                    {
                        GoToLocation(p, location);
                        break;
                    }
                }
            }
            else
            {
                WrapText($"That path is way too steep to climb without something to help you keep your balance.");
                GoToLocation(p, currentLocation);
            }
        }

        private void DanCheck(Player p, Item item)
        {
            if (p.PlayerLocation.Characters.ContainsKey("danReading"))
            {
                TransferItem(p, item);
            }
            else
            {
                WrapText(p.PlayerLocation.Items["grease"].Actions["blocked"]);
            }
        }

        private void BlackberryCheck(Player p, Item item, List<Item> items)
        {
            if (p.Inventory.ContainsKey("emptyCan"))
            {
                foreach (Item i in items)
                {
                    if (i.Name == "canOfBerries")
                    {
                        p.Inventory.Add(i.Name, i);
                        p.Inventory.Remove(item.Name);
                        WrapText(i.Actions["blocked"]);
                    }
                }
            }
            else
            {
                WrapText(p.PlayerLocation.Items["blackberries"].Actions["blocked"]);
            }
        }

        private void TransferItem(Player p, Item item)
        {
            p.Inventory.Add(item.Name, item);
            p.PlayerLocation.Items.Remove(item.Name);
            TypeLine(item.Actions["get"]);
        }

        private void SwitchChar(Player p, Item item, Character character, Dictionary<string, Character> characters, string switchTo)
        {
            foreach (Character c in characters.Values)
            {
                if (c.Name == switchTo)
                {
                    p.Inventory.Remove(item.Name);
                    p.PlayerLocation.Characters.Remove(character.Name);
                    p.PlayerLocation.Characters.Add(c.Name, c);
                }
            }

        }

        private void CannotVerbNoun(string verb, string noun)
        {
            WrapText($"You can't {verb} {noun} ");
        }

        private void TypeLine(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                Console.Write(line[i]);
                Thread.Sleep(textLoadSpeed);
                if (Console.KeyAvailable)
                {
                    textLoadSpeed = 0;
                }
            }
            textLoadSpeed = userSpeed;
            Console.WriteLine();
        }

		private void GameOverMan(Player player, string description)
		{
			WrapText(description);
			player.GameIsRunning = false;
		}
		
	}

}




