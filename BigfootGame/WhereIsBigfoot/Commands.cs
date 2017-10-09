using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
    // commands are in alpha order
    // helper functions at bottom 

    // TODO: null check for all 

    public class Commands
    {
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

        //TODO special cases
        // special cases
        // can't take tin can if dan is not reading 
        // can't take blackberries unless they have the empty can 
        // - hard code response to blackberries 
        // - if have can, can add can full of blackberries to inventory
        // - if not then eat blackberries 
        // can get octopus 
        // hardcode responses 
        public void Get(Player p, Asset a)
        {
            if (p.PlayerLocation.Items.ContainsKey(a.Name))
            {
                Item item = (Item)a;

                if (p.PlayerLocation.Items[item.Name].Actions.ContainsKey("get"))
                {
                    if (item.Target == "danReading")
                    { }
                    else if (item.Target == "emptyCan")
                    { }
                    TransferItem(p, item);
                }
                else
                {
                    CannotVerbNoun("get", item.Name);
                    TypeLine($"That {item.Name} is not in this location.");
                }
            }
            else if (p.PlayerLocation.Characters.ContainsKey(a.Name))
            {
                Character character = (Character)a;
                if (p.PlayerLocation.Characters[character.Name].Actions.ContainsKey("get"))
                {
                    TypeLine(p.PlayerLocation.Characters[character.Name].Actions["get"]);
                }
                else
                {
                    CannotVerbNoun("get", a.Name);
                    TypeLine($"Getting {a.Name} would be rude and they do not fit in your backpack.");
                }
            }
            else
            {
                CannotVerbNoun("get", a.Name);
                TypeLine($"Let's face it, you just have to let go and move on.");
            }
        }

        // DONE takes player, item, two characters
        // see RESULT in SwitchChar 
        // player, item as an item, character as a character
        // character dictionary passed 
        public void Give(Player p, Item item, Character character, Dictionary<string, Character> characters)
        {
            // check to see if character is target
            if (item.Target == "danCooking" | item.Target == "bigfootHostile")
            {
                // give Dan book
                if (item.Name == "book" && character.Name == "danCooking")
                {
                    SwitchChar(p, item, character, characters, "danCooking");
                }
                // RESULT check if bigfootHostile is removed from characters dict in player location
                // RESULT check if bigfootFriendly is in characters dict in player location
                else if (item.Name == "bacon" && character.Name == "bigfootHostile")
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
                foreach (Location location in locations)
                {
                    if (location.Name == newLocation)
                    {
                        p.PlayerLocation = location;
                        Console.Title += $"? -- {location.DescriptionShort}";
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

        // player, item, asset 
        // check if asset is target
        public void Use(Player p, Item item, Asset asset)
        {
            Location location = p.PlayerLocation;
            foreach (Item itemToUse in p.Inventory.Values)
            {
                if (itemToUse.ParseValue.Contains(item.Name))
                {
                    foreach (Item i in location.Items.Values)
                    {
                        if (itemToUse.Name == "book" | i.Target == asset.Name)
                        {
                            TypeLine(WrapText(itemToUse.Actions["use"]));
                        }
                        else
                        {
                            CannotVerbNoun("use", item.Name);
                        }
                    }
                }
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

        // write this nicely
        // use "title"
        public void Inventory(Player p)
        {
            TypeLine("You have the following inventory: ");
            foreach (var item in p.Inventory.Values)
            {
                TypeLine($"{item.Title}");
            }

        }

        // done
        public void Look(Player p, string entry)
        {
            foreach (Item item in p.Inventory.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    TypeLine(WrapText($"{item.DescriptionLong} \n"));
                    return;
                }
            }
            foreach (Item item in p.PlayerLocation.Items.Values)
            {
                if (item.ParseValue.Contains(entry))
                {

                    TypeLine(WrapText($"{item.DescriptionLong} \n"));
                    return;
                }
            }
            foreach (Character character in p.PlayerLocation.Characters.Values)
            {
                if (character.ParseValue.Contains(entry))
                {

                    TypeLine(WrapText($"{character.DescriptionLong} \n"));
                    return;
                }
            }

            TypeLine(WrapText($"{p.PlayerLocation.Exits["text"]}"));

            if (entry == "none")
            {
                TypeLine(WrapText($"{p.PlayerLocation.DescriptionLong} \n"));
                string descriptions = "";
                foreach (Character character in p.PlayerLocation.Characters.Values)
                    descriptions += character.DescriptionShort;
                foreach (Item item in p.PlayerLocation.Items.Values)
                    descriptions += item.DescriptionShort;
                TypeLine(WrapText($"{p.PlayerLocation.Exits["text"]}"));

                if (descriptions != "")
                    TypeLine(WrapText($"{descriptions}"));
                return;
            }
            TypeLine($"I don't see {entry} here {p.PlayerHair}ie. ");
        }

        // write like use 
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

        // character
        public void Talk(Player p, String name, Dictionary<string, Character> characters)
        {
            foreach (Character c in characters.Values)
            {
                if (p.PlayerLocation.Characters.ContainsKey(name))
                {
                    TypeLine(WrapText(c.Actions["talk"]));
                }
                else
                {
                    TypeLine("This character does not exist in this location.");
                }
            }
        }

        // write better help
        // description 
        public void Help(Player p)
        {
            TypeLine($"Hey {p.PlayerHair} hair, I dont freaking understand that! Use a 2 word command format: ");
            TypeLine($"ie. get item -or- go north");
            TypeLine($"Possible commands for {p.PlayerName}: get, go, give, use, talk, put, help, quit, inventory");
            TypeLine($"Trying to figure out where you are? Your current location is displayed in the title bar at the top of your the game's console window. Also, entering the command \"look\" in any location will give you a description of that location.");
        }

        private void TransferItem(Player p, string item)
        {
            Item itemToTransfer = p.PlayerLocation.Items[item];
            p.Inventory.Add(item, itemToTransfer);
            p.PlayerLocation.Items.Remove(item);
            TypeLine(WrapText(itemToTransfer.Actions["get"]));
        }

        // helper method which transfer item from location to inventory
        // then prints "get" text 
        private void TransferItem(Player p, Item item)
        {
            if (p.PlayerLocation.Items.ContainsValue(item))
            {
                p.Inventory.Add(item.Name, item);
                p.PlayerLocation.Items.Remove(item.Name);
                TypeLine(item.Actions["get"]);
            }
        }

        private void SwitchChar(Player p, Item item, Character character, Dictionary<string, Character> characters, string switchTo)
        {
            foreach (Character c in characters.Values)
            {
                if(c.Name == switchTo)
                {
                    p.Inventory.Remove(item.Name);
                    p.PlayerLocation.Characters.Remove(character.Name);
                    p.PlayerLocation.Characters.Add(c.Name, c);
                }
            }

        }

        public void ShowLocation(Location location)
        {
            if (location.Visited == false)
            {
                TypeLine(WrapText($"{location.DescriptionFirst}"));

                foreach (Character character in location.Characters.Values)
                    TypeLine(WrapText($"{character.DescriptionFirst}"));

                foreach (Item item in location.Items.Values)
                    TypeLine(WrapText($"{item.DescriptionFirst}"));

                TypeLine(WrapText($"{location.Exits["text"]}"));


                location.Visited = true;
            }
            else
            {
                TypeLine(WrapText($"{location.DescriptionShort}"));

                foreach (Character character in location.Characters.Values)
                    Console.WriteLine(WrapText($"{character.DescriptionShort}"));

                foreach (Item item in location.Items.Values)
                    TypeLine(WrapText($"{item.DescriptionShort}"));

                TypeLine(WrapText($"{location.Exits["text"]}"));
            }
        }

        private void CannotVerbNoun(string verb, string noun)
        {
           TypeLine($"You can't {verb} {noun} ");
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


        public string WrapText(String text)
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




