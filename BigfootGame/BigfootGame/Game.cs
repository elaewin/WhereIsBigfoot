using System;
using Newtonsoft.Json;


namespace BigfootGame
{
    class Game
    {
        // Parsing (Erika + Tyler) 

        // Populate Location Items and Populate characters dictionary 

        // product of parser is dictionary<string, string> 

        
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            

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
            // read file into a string and deserialize JSON to a type
            //Location location1 = JsonConvert.DeserializeObject<Movie>(File.ReadAllText(@"c:\location.json"));

            // deserialize JSON directly from a file
            //using (StreamReader file = File.OpenText(@"c:\location.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    Location location2 = (Movie)serializer.Deserialize(file, typeof(Movie));
            //}


        }
    }
}
