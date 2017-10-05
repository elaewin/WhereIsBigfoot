using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WhereIsBigfoot
{
    class Commands
    {
        // use - 
        // talk to
        // put - two items interacting with each other


        public void Go(Player p, string direction, List<Location> locations)
        {
            Location currentLocation = p.PlayerLocation;
            string newLocation;

            if (currentLocation.Exits.ContainsKey(direction))
            {
                newLocation = currentLocation.Exits[direction];
                //foreach (Location location in locations)
                //{
                //    if (location.Name == newLocation)
                //    {
                //        p.PlayerLocation = location;
                //    }
                //}
            }
            else
            {
                Console.WriteLine("You run into an impenetrable barrier and must return.");
            }

        }

        //public void Get(Player p, string item)
        //{
        //    if (p.PlayerLocation.Items.ContainsKey(item))
        //    {
                
        //        TransferItem(p, item);
        //    }
        //    else
        //    {
        //        Console.WriteLine($"There is no {item}");
        //    }
        //}

        //public void Drop(Player p, string item)
        //{
        //    p.PlayerLocation.Items.Add(item, p.Inventory[item]);
        //    p.Inventory.Remove(item);
        //    Console.WriteLine($"You dropped, {item}");
        //}

        public void Give(Player p, string item, Character c)
        {
            // 
        }

        //private void TransferItem(Player p, string item)
        //{
        //    p.Inventory.Add(item, p.PlayerLocation.Items[item]);
        //    p.PlayerLocation.Items.Remove(item);
        //}

        // TODO: alter json to reflect abstracted method for stuff inheriting from parent
        // TODO: asset class

        //private void TransferObject(Player p, string item, List<Asset> list, string name)
        //{
        //    foreach (Asset obj in list)
        //    {
        //        if (obj.Name == name)
        //        {
        //            p.PlayerLocation = (obj.GetType())obj;
        //        }
        //    }
        //}

        private void ChangeLocation(Player p, Asset obj)
        {
            p.PlayerLocation = (Location)obj;
        }

        //private void DanCheck(Player p, string item, List<Character> characters)
        //{
        //    if (p.PlayerLocation.Name == "dan")
        //    {
        //        if(p.PlayerLocation.Characters.ContainsKey("danCooking"))
        //        {
        //            TransferItem(p, item);
        //            foreach (Character c in characters)
        //            {
        //                if()
        //            }
        //        }
        //    }
        //}

        public void Help(Player p)
        {
            Console.WriteLine($"Hey {p.PlayerHair}ie, I dont freaking understand that! Use a 2 word command format: ");
            Console.WriteLine($"ie. grab item -or- go north");
            Console.WriteLine($"Possible commands for {p.PlayerName}: get, go, give, use, talk, put, help, quit, inventory");
            if (p.Inventory != null)
                Console.Write("You have the following inventory: ");
                foreach (var value in p.Inventory)
                {
                    Console.Write($"{value} ");
                }
        }
    }
}
