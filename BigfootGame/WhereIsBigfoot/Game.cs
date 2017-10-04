using System;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.IO;


namespace BigfootGame
{
    class Game
    {
		//
        public List<Location> locations;
        
        static void Main(string[] args)
        {
            
            Game g = new Game();

			//Deserialize JSON from a file:

            string jsonFile = @"..\..\locations.json";
           
            g.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonFile));

            foreach(Location location in g.locations) 
                Console.WriteLine(location.LocationName);


        }
    }
}
