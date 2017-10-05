using System;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.IO;


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
            foreach (Location location in game.locations)
                Console.WriteLine(location.Name);
            foreach (Item item in game.items)
                Console.WriteLine(item.Name);
            foreach (Character character in game.characters)
                Console.WriteLine(character.CharacterName);

            foreach (Location location in game.locations)
            {
                foreach (Item item in game.items)
                {
                        if (location.Items.ContainsKey(item.Name))
                        {
                            location.Items[item.Name] = item;
                            Console.WriteLine("=======");
                        }
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
