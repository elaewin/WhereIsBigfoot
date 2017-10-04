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
        public void LoadData(Game game) {
            string jsonFile = @"../../locations.json";

            string characterFile = @"../../characters.json";

            string itemFile = @"../../items.json";

            game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonFile));

            game.characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(characterFile));

            game.items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(itemFile));

            foreach (Location location in game.locations)
                Console.WriteLine(location.LocationName);

            foreach (Character character in game.characters)
                Console.WriteLine(character.CharacterName);

            foreach (Item item in game.items)
                Console.WriteLine(item.ItemName);

        }

        static void Main(string[] args)
        {
            
            Game game = new Game();

            game.LoadData(game);

        }
    }
}
