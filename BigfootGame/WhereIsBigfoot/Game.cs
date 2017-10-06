using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using Newtonsoft.Json;
using static System.Console;


namespace WhereIsBigfoot
{
	public class Game
	{
		private List<Location> locations;
		private List<Item> items;
		private List<Character> characters;
		List<string> allowedVerbs = new List<string>() { "get", "go", "give", "look", "use", "talk", "put", "help", "quit", "inventory", "drop" };

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
			//TypeLine("Locations:");
			//foreach (Location location in game.locations)
			//    TypeLine(location.Name);

			//TypeLine("\nItems:");
			//foreach (Item item in game.Items) { 
			//    TypeLine($"Item {item.Name} is in location {item.Location}");
			//    foreach (string word in item.ParseValue)
			//        commands.TypeLine(word);
			//}
			//TypeLine("\nCharacters:");
			//foreach (Character character in game.characters)
			//TypeLine(character.CharacterName);

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
						//TypeLine($"Assigned item key {key} on location {location.Name}");
					}
				}
			}

			//// Test assignment of items by count of the items dict on the location.
			//TypeLine("\nLocation Items count:");
			//foreach (Location location in game.locations)
			//TypeLine($"Location: {location.Name}, # of items: {location.Items.Count}");

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
						//TypeLine($"Assigned character with key {key} on location {location.Name}");
					}
				}
			}

			//// Test assignment of characters by count of the characters dict on the location.
			//TypeLine("\nLocation Characters count:");
			//foreach (Location location in game.locations)
			//TypeLine($"Location: {location.Name}, # of items: {location.Characters.Count}");

			// Test existence of exits dict by count of the characters array on the location.
			//TypeLine("\nLocation Exits count:");
			//foreach (Location location in game.locations)
			//TypeLine($"Location has {location.Exits.Count}");

		}

		public void ParseInput(string prompt)
		{
			string input = GetInput(prompt).ToLower().Trim();

			if (IsValidInput(input) && (input != "") && input != null)
			{
				string[] parsed = input.Split(default(string[]), 2, StringSplitOptions.RemoveEmptyEntries);
				string verb = parsed[0];

				if (parsed.Length == 0)
				{
					commands.TypeLine($"Sorry, {this.Player.PlayerName} I didn't catch that.");
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
						//case "give":
						//	string giveTarget = GetInput($"Who do you want to give {parsed[1]}?");
						//	commands.Give(Player, parsed[1], Player.PlayerLocation.Characters, giveTarget);
						//	break;
						case "look":
							commands.Look(Player, parsed[1]);
							break;
						case "use":
							string useTarget = GetInput($"What do you want to use {parsed[1]} on?");
							commands.Use(Player, parsed[1], useTarget);
							break;
						//case "talk":
						//	commands.Talk(Player, parsed[1], Player.PlayerLocation.Characters);
						//	break;
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
							string verify = GetInput("Are you sure you want to quit? y/n: ").ToLower();
							if (verify == "y" || verify == "yes")
							{
								this.running = false;
								WriteLine();
								commands.TypeLine("Thank you for playing Where is Bigfoot!");
								WriteLine();
								break;
							}
							else
							{
								WriteLine("Quit game cancelled.");
								break;
							}

						default:
							commands.Help(Player);
							break;
					}
				}
				else
				{
					WriteLine("I'm sorry, I didn't understand that. For a list of usable verbs, type \"help\".");
				}
				if (parsed[0] != "look" && parsed[0] != "quit" && parsed[0] != "go")
					WriteLine($"{this.Player.PlayerLocation.DescriptionShort}");
			}
			else
			{
				WriteLine("I'm sorry, I didn't understand that. For a list of usable verbs, type \"help\".");
			}
		}

		// Console formatting
		public void FormatConsole()
		{
			Console.Title = "Where Is Bigfoot?";
			Console.CursorVisible = true;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.BackgroundColor = ConsoleColor.Black;
		}

		public void StartGame()
		{
			WriteLine("TEAM BEE DEVELOPMENT PRESENTS:");
			WriteLine(@"
 __          ___                     _     
 \ \        / / |                   (_)    
  \ \  /\  / /| |__   ___ _ __ ___   _ ___ 
   \ \/  \/ / | '_ \ / _ \ '__/ _ \ | / __|
    \  /\  /  | | | |  __/ | |  __/ | \__ \
  ___\/ _\/   |_|_|_|\___|_|  \___|_|_|___/
 |  _ \(_)      / _|          | ||__ \     
 | |_) |_  __ _| |_ ___   ___ | |_  ) |    
 |  _ <| |/ _` |  _/ _ \ / _ \| __|/ /     
 | |_) | | (_| | || (_) | (_) | |_|_|      
 |____/|_|\__, |_| \___/ \___/ \__(_)      
           __/ |                           
          |___/                            
");
			WriteLine(GetWordWrappedParagraph("Where is Bigfoot is a text-based adventure game where you take on the role of a camper who is trying to find that most elusive of cryptids: BIGFOOT!"));
			WriteLine("");
			WriteLine("First, you need to decide who you are... \n");
			//string startGame = GetInput("Would you like to start the game? (y/n): ");
			//if (startGame == "yes" || startGame == "y") {
			//	return;
			//	}
			//else if (startGame == "no" || startGame == "n")
			//{
			//	WriteLine("See you later!");

			//}
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

			do
			{
				gender = GetInput("What gender are you? ");
			} while (!IsValidInfo(gender));

			do
			{
				hair = GetInput("Okay, now just so we know, what color is your hair? ");
			} while (!IsValidInfo(hair));

			string[] deets = new string[3] { name, gender, hair };

			commands.TypeLine("\nNow that that's done with...");
			return deets;
		}

		static void Main(string[] args)
		{

			Game game = new Game();
			game.FormatConsole();
			game.LoadData(game);

			game.StartGame();

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

			game.commands.TypeLine(GetWordWrappedParagraph("Your old buddy, Dan, from college was always crazy about finding Bigfoot. You even went on a couple of Bigfoot hunting expeditions with him way back when. But you weren't expecting him to contact you out of the blue and invite you on another one, now that it's been years since you graduated. But the memory of how relaxing those previous trips were made you agree to do along.\n\nYou drove out from Seattle last night, and into the wilderness between Mount Rainier and Mount St. Helens. You set up your camp near the area where Dan said he'd been camping (you hope--the directions weren't exactly great), and crashed for the night. Now it's morning. Time to find your old buddy."));
			WriteLine();
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

		public static string GetWordWrappedParagraph(string paragraph)
		{
			if (string.IsNullOrWhiteSpace(paragraph))
			{
				return string.Empty;
			}

			var approxLineCount = paragraph.Length / Console.WindowWidth;
			var lines = new StringBuilder(paragraph.Length + (approxLineCount * 4));

			for (var i = 0; i < paragraph.Length;)
			{
				var grabLimit = Math.Min(Console.WindowWidth, paragraph.Length - i);
				var line = paragraph.Substring(i, grabLimit);

				var isLastChunk = grabLimit + i == paragraph.Length;

				if (isLastChunk)
				{
					i = i + grabLimit;
					lines.Append(line);
				}
				else
				{
					var lastSpace = line.LastIndexOf(" ", StringComparison.Ordinal);
					lines.AppendLine(line.Substring(0, lastSpace));

					//Trailing spaces needn't be displayed as the first character on the new line
					i = i + lastSpace + 1;
				}
			}
			return lines.ToString();

		}
	}
}
