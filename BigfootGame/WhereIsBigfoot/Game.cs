using System;
using Newtonsoft.Json;
using System.Data;
using System.Collections.Generic;
using System.IO;


namespace BigfootGame
{
    class Game
    {
        public List<Location> location;
        // Parsing (Erika + Tyler) 

        // Populate Location Items and Populate characters dictionary 

        // product of parser is dictionary<string, string> 

        
        
        static void Main(string[] args)
        {
            

            //Serialize a Location:

            //Location location = new Location
            //{
                //Email = "james@example.com",
                //Active = true,
                //CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                //Roles = new List<string>
                //{
                //"User",
                //"Admin"
                //}
            //};

            //string json = JsonConvert.SerializeObject(account, Formatting.Indented);
            // {
            //   "Email": "james@example.com",
            //   "Active": true,
            //   "CreatedDate": "2013-01-20T00:00:00Z",
            //   "Roles": [
            //     "User",
            //     "Admin"
            //   ]
            // }

            //Console.WriteLine(json);


            //Deserialize JSON from a file:

            string jsonFile = @"locations.json";
           
            List<Location> location = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonFile));

            foreach(Location location in locations) 
                Console.WriteLine(location.locationName);


        }
    }
}
