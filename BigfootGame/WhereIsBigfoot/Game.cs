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

            game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonFile));

            foreach (Location location in game.locations)
                Console.WriteLine(location.LocationName);
        }

        static void Main(string[] args)
        {
            
            Game game = new Game();

            game.LoadData(game);

        }
    }
}
