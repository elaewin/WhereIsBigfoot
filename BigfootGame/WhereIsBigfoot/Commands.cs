﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
    class Commands
    {
        // TODO: check if action is available 

        // TODO: Write Go Method
        // TODO: Use actions text 
        // TODO: parse value

        public void Use(Player p, string item)
        {
            if (p.Inventory.ContainsKey(item))
            {
                Item itemToUse = p.Inventory[item];
                if (itemToUse.Actions.ContainsKey("use"))
                {
                    Console.WriteLine(itemToUse.Actions["use"]);
                }
                else
                {
                    CannotVerbNoun("use", item);
                }
            }
            else
            {
                CannotVerbNoun("use", item);
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
                    Console.WriteLine(p.PlayerLocation.Items[name].Actions["get"]);
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
                    Console.WriteLine(p.PlayerLocation.Characters[name].Actions["get"]);
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
            Console.WriteLine($"You dropped, {item}");
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
        public void TalkTo(Player p, Character c)
        {
            if (p.PlayerLocation.Characters.ContainsKey(c.Name))
            {
                Console.WriteLine(c.Actions["talk"]);
            }
            else
            {
                Console.WriteLine("This character does not exist in this location.");
            }
        }

        public void Put(Player p, List<Item> items)
        {
            if (p.Inventory.ContainsKey("lantern") && p.Inventory.ContainsKey("grease"))
            {
                Console.WriteLine("Now your lantern is full and you can use it to go in the cave.");
                p.Inventory.Remove("lantern");
                foreach (Item i in items)
                {
                    if (i.Name == "filledLantern")
                        p.Inventory.Add("filledLantern", i);
                }
            }
            else
            {
                Console.WriteLine("You need to have the lantern and the grease in your inventory before you can use it.");
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

        public void LookAt(Player p, string entry)
        {
            foreach (Item item in p.Inventory.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    Console.Write($"{item.DescriptionLong} ");
                }
            }
            foreach (Item item in p.PlayerLocation.Items.Values)
            {
                if (item.ParseValue.Contains(entry))
                {
                    Console.Write($"{item.DescriptionLong} ");
                }
            }
            foreach (Character character in p.PlayerLocation.Characters.Values)
            {
                if (character.ParseValue.Contains(entry))
                {
                    Console.Write($"{character.DescriptionLong} ");
                }
            }
        }

        private void CannotVerbNoun(string verb, string noun)
        {
            Console.WriteLine($"You can't {verb} {noun}");
        }
    }
}

}
