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
            //WriteLine("Locations:");
            //foreach (Location location in game.locations)
            //    WriteLine(location.Name);
            
            //WriteLine("\nItems:");
            //foreach (Item item in game.items)
            //    WriteLine($"Item {item.Name} is in location {item.Location}");
            
            //WriteLine("\nCharacters:");
            //foreach (Character character in game.characters)
                //WriteLine(character.CharacterName);

            // Go through each item and assign the item to the items dict in each location, 
            // based on the location property of the item.
            foreach(Item item in game.items) {
                string name = item.Location;
                string key = item.Name;

                foreach(Location location in game.locations) {
                    if (location.Name == name) {
                        location.Items.Add(key, item);
                        //WriteLine($"Assigned item key {key} on location {location.Name}");
                    }
                }
            }

            //// Test assignment of items by count of the items dict on the location.
            //WriteLine("\nLocation Items count:");
            //foreach (Location location in game.locations)
                //WriteLine($"Location: {location.Name}, # of items: {location.Items.Count}");
            
            // Go through each character and assign the character to the character dict in each location, 
            // based on the location property of the characger.
            foreach (Character character in game.characters)
            {
                string name = character.Location;
                string key = character.Name;

                foreach (Location location in game.locations)
                {
                    if (location.Name == name)
                    {
                        location.Characters.Add(key, character);
                        //WriteLine($"Assigned character with key {key} on location {location.Name}");
                    }
                }
            }

            // Test assignment of characters by count of the characters dict on the location.
            //WriteLine("\nLocation Characters count:");
            //foreach (Location location in game.locations)
                //WriteLine($"Location: {location.Name}, # of items: {location.Characters.Count}");

            // Test existence of exits dict by count of the characters array on the location.
            //WriteLine("\nLocation Exits count:");
            //foreach (Location location in game.locations)
                //WriteLine($"Location has {location.Exits.Count}");

        }

        static void Main(string[] args)
        {

            Game game = new Game();

            game.LoadData(game);

        }
    }
}
