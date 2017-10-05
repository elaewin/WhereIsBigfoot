using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static System.Console;


namespace WhereIsBigfoot
{
	public class Game
	{
		private List<Location> locations;
		private List<Item> items;
		private List<Character> characters;
		List<string> allowedVerbs = new List<string>() { "get", "go", "give", "use", "talk", "put", "help", "quit", "inventory" };

		private Player player;

		private string currentVerb;
		private string currentNoun;
		private string currentSubject;
		bool running = true;

		public string CurrentVerb
		{
			get => this.currentVerb;
			set => this.currentVerb = value;
		}

		public string CurrentNoun
		{
			get => this.currentNoun;
			set => this.currentNoun = value;
		}

		public string CurrentSubject
		{
			get => this.currentSubject;
			set => this.currentSubject = value;
		}

		public Player Player
		{
			get => this.player;
			set => this.player = value;
		}

		public List<Location> Locations
		{
			get => this.locations;
			set => this.locations = value;
		}

		public List<Item> Items
		{
			get => this.items;
			set => this.items = value;
		}

		// Deserialize JSON from a file. 
		public void LoadData(Game game)
		{
			string jsonLocationFile = @"../../locations.json";
			string jsonItemFile = @"../../items.json";
			string jsonCharacterFile = @"../../characters.json";

			game.items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(jsonItemFile));
			game.characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(jsonCharacterFile));
			game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonLocationFile));

			////Testing to make sure the objects are being de-serialized by writing them to the console.
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
			foreach (Item item in game.items)
			{
				string name = item.Location;
				string key = item.Name;

				foreach (Location location in game.locations)
				{
					if (location.Name == name)
					{
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

			//// Test assignment of characters by count of the characters dict on the location.
			//WriteLine("\nLocation Characters count:");
			//foreach (Location location in game.locations)
			//WriteLine($"Location: {location.Name}, # of items: {location.Characters.Count}");

			// Test existence of exits dict by count of the characters array on the location.
			//WriteLine("\nLocation Exits count:");
			//foreach (Location location in game.locations)
			//WriteLine($"Location has {location.Exits.Count}");

		}

		public void ParseInput(string prompt)
		{
			string input = GetInput(prompt).ToLower().Trim();

			if (IsValidInput(input))
			{
				string[] split = input.Split(default(string[]), 2, StringSplitOptions.RemoveEmptyEntries);

				string verb = split[0];

				if (allowedVerbs.Contains(verb))
				{
					// Needs logic on how to use each verb.
					this.CurrentVerb = verb;
					WriteLine($"Current Verb: {this.currentVerb}");
				}
				else
				{
					// call help method?
					WriteLine("I'm sorry, I didn't understand that. For a list of usable verbs, type \"help\".");
					ParseInput(prompt);
				}
				if (split.Length == 2)
				{
					this.CurrentNoun = split[1];
					WriteLine($"Current Noun: {this.currentNoun}");
				}
				if (verb == "quit")
				{
					this.running = false;
				}
			}
			else
			{
				WriteLine("I'm afraid I didn't understand that.");
				GetInput(prompt);
			}
		}

        // TODO: Execute Command Method 

		// Console formatting
		public void FormatConsole()
		{
			Console.Title = "Where Is Bigfoot?";
			Console.CursorVisible = true;
		}

		public string[] GetPlayerDetails()
		{
			string name = GetInput("What is your name? ");
			string gender = GetInput("What gender are you? ");
			string hair = GetInput("Okay, now just so we know, what color is your hair? ");
			string[] deets = new string[3] { name, gender, hair};
			return deets;
		}

		static void Main(string[] args)
		{

			Game game = new Game();
			game.FormatConsole();
			game.LoadData(game);

			// create Player instance
			string[] playerDetails = game.GetPlayerDetails();
			Player newPlayer = new Player(playerDetails[0], playerDetails[1], playerDetails[2]);

			foreach (Location location in game.Locations)
			{
				if (location.Name == "tent")
					newPlayer.PlayerLocation = location;
			}
			foreach (Item item in game.Items)
			{
				if (item.Name == "cellPhone")
					newPlayer.Inventory.Add("cellPhone", item);
			}

			// Assign Player instance to game
			game.Player = newPlayer;
			
			game.ParseInput("> ");

		}

		public static bool IsValidInput(string str)
		{
			Regex regexString = new Regex(@"[a-z\s']*$");
			return regexString.IsMatch(str);
		}

		public static string GetInput(string prompt)
		{
			Console.Write(prompt);
			string input = ReadLine();
			return input;
		}

	}
}
