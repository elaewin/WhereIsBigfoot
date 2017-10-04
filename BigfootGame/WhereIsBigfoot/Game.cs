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
            string jsonLocationFile = @"../../locations.json";
            string jsonItemFile = @"../../items.json";
            string jsonCharacterFile = @"../../characters.json";

            game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonLocationFile));
            game.items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(jsonItemFile));
            game.characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(jsonCharacterFile));



            foreach (Location location in game.locations)
                Console.WriteLine(location.LocationName);
            foreach (Item item in game.items)
                Console.WriteLine(item.ItemName);
            foreach (Character character in game.characters)
                Console.WriteLine(character.CharacterName);

        }

        static void Main(string[] args)
        {
            
            Game game = new Game();

            game.LoadData(game);

        }
    }
}
