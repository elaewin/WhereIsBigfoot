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
		List<string> allowedVerbs = new List<string>() { "get", "go", "give", "look", "use", "talk", "put", "help", "quit", "inventory" };

		Commands commands = new Commands();

		private Player player;

		bool running = true;

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
			//foreach (Item item in game.Items) { 
			//    WriteLine($"Item {item.Name} is in location {item.Location}");
			//    foreach (string word in item.ParseValue)
			//        Console.WriteLine(word);
			//}
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
			Console.WriteLine(input);

			if (IsValidInput(input) && (input != "") && input != null)
			{
				string[] parsed = input.Split(default(string[]), 2, StringSplitOptions.RemoveEmptyEntries);
				string verb = parsed[0];

				if (parsed.Length == 0)
				{
					Console.WriteLine($"Sorry, {this.Player.PlayerName} I didn't catch that.");
					return;
				}

				if (parsed.Length == 1)
					parsed = new string[2] { parsed[0], "none" };

				if (allowedVerbs.Contains(verb))
				{
					// Needs logic on how to use each verb.
					switch (verb)
					{
						case "go":
							commands.Go(Player, parsed[1], this.Locations);
							break;
						case "get":
							commands.Get(Player, parsed[1]);
							break;
						case "give":
							string giveTarget = GetInput($"Who do you want to give {parsed[1]}?")
							commands.Give(Player, parsed[1], Player.PlayerLocation.Characters, giveTarget);
							break;
						case "look":
							commands.Look(Player, parsed[1]);
							break;
						case "use":
							string useTarget = GetInput($"What do you want to use {parsed[1]} on?")
							commands.Use(Player, parsed[1], useTarget);
							break;
						case "talk":
							string getTarget = GetInput($"Who do you want to talk to?");
							commands.Talk(Player, parsed[1], Player.PlayerLocation.Characters, getTarget);
							break;
						case "put":
							commands.Put(Player, parsed[1], items);
							break;
						case "help":
							commands.Help(Player);
							break;
						case "inventory":
							commands.Inventory(Player);
							break;
						case "quit":
							this.running = false;
							break;
						default:
							commands.Help(Player);
							break;
					}
				}
				else
				{
					WriteLine("I'm sorry, I didn't understand that. For a list of usable verbs, type \"help\". \n");
				}
				if (parsed[0] != "look" && parsed[0] != "quit" && parsed[0] != "go")
					WriteLine($"{this.Player.PlayerLocation.DescriptionShort} \n");
			}
			else
			{
				WriteLine("I'm sorry, I didn't understand that. For a list of usable verbs, type \"help\". \n");
			}
		}

		// TODO: Execute Command Method 

		// Console formatting
		public void FormatConsole()
		{
			Console.Title = "Where Is Bigfoot?";
			Console.CursorVisible = true;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.BufferHeight = 200;
			Console.BufferWidth = 300;
		}

		public string[] GetPlayerDetails()
		{
			string name = "";
			string gender = "";
			string hair = "";

			do
			{
				name = GetInput("What is your name? ");
			} while (!IsValidInfo(name));

			do {
				gender = GetInput("\nWhat gender are you? ");
			} while (!IsValidInfo(gender));

			do
			{
			hair = GetInput("\nOkay, now just so we know, what color is your hair? ");
			} while (!IsValidInfo(hair));

			string[] deets = new string[3] { name, gender, hair };
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

			// Show starting room
			Console.WriteLine();
			game.commands.ShowLocation(game.Player.PlayerLocation);

			do
			{
				game.ParseInput("> ");

			} while (game.running == true);
		}

		public static bool IsValidInput(string str)
		{
			Regex regexString = new Regex(@"[a-z\s'\r]*$");
			return regexString.IsMatch(str);
		}

		public static bool IsValidInfo(string str)
		{
			Regex regexString = new Regex(@"[a-zA-Z\s]*$");
			return regexString.IsMatch(str);
		}

		public static string GetInput(string prompt)
		{
			Console.Write(prompt);
			string input = ReadLine();
			if (input == null)
				return "";
			return input;
		}

	}
}
