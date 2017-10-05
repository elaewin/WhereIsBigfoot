using System;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.IO;
using static System.Console;


namespace WhereIsBigfoot
{
    class Game
    {
        //
        public List<Location> locations;
        public List<Item> items;
        public List<Character> characters;

        // Deserialize JSON from a file. 
        public void LoadData(Game game)
        {
            string jsonLocationFile = @"../../locations.json";
            string jsonItemFile = @"../../items.json";
            string jsonCharacterFile = @"../../characters.json";

            game.items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(jsonItemFile));
            game.characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(jsonCharacterFile));
            game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonLocationFile));

            //Testing to make sure the objects are being de-serialized by writing them to the console.
            WriteLine("Locations:");
            foreach (Location location in game.locations)
                WriteLine(location.Name);
            WriteLine("\nItems:");
            foreach (Item item in game.items)
                WriteLine(item.Name);
            WriteLine("\nCharacters:");
            foreach (Character character in game.characters)
                WriteLine(character.CharacterName);

            WriteLine("\nItems count per location:");
            foreach (Location location in game.locations)
            {
                foreach (Item item in game.items)
                {
                    WriteLine($"Location:D {location.Name} - Items: {location.Items.Count}");
                        
                        //if (location.Items.ContainsKey(item.Name))
                        //{
                        //    location.Items[item.Name] = item;
                        //    WriteLine("=======");
                        //}
                }

                foreach (var value in location.Items.Values)
                {
                    Console.WriteLine("Value of the Dictionary Item is: {0}", value.ToString());
                }
            }

        }

        static void Main(string[] args)
        {

            Game game = new Game();

            game.LoadData(game);

        }
    }
}
