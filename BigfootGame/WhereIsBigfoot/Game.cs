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
		private Dictionary<string, string> parseDict;
		List<string> allowedVerbs = new List<string>() { "drop", "get", "go", "give", "look", "use", "talk", "put", "help", "quit", "inventory" };

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

		public Dictionary<string, string> ParseDict
		{
			get => this.parseDict;
			set => this.parseDict = value;
		}

		// Deserialize JSON from a file. 
		public void LoadData(Game game)
		{
			string jsonLocationFile = @"../../locations.json";
			string jsonItemFile = @"../../items.json";
			string jsonCharacterFile = @"../../characters.json";
			string parseDictFile = @"../../parseDictionary.json";

			game.items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(jsonItemFile));
			game.characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(jsonCharacterFile));
			game.locations = JsonConvert.DeserializeObject<List<Location>>(File.ReadAllText(jsonLocationFile));
			game.parseDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(parseDictFile));

			// Go through each item and assign the item to the items dict in each location based on the location property of the item.
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

			// Go through each character and assign the character to the character dict in each location, based on the location property of the character.
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
		}

		// HELPER METHODS

		// Check if an item is in a given dictionary of items and return that item or null
		private Item DoesItemExistIn(Dictionary<string, Item> itemsDict, string itemName)
		{
			foreach (Item item in itemsDict.Values)
			{
				if (item.ParseValue.Contains(itemName))
					return item;
				
			}
			return null;
		}

		// Check if a character is in a location and return that character or null
		private Character CharacterExistsIn(Location location, string characterName)
		{
			foreach (Character character in location.Characters.Values)
			{
				if (character.ParseValue.Contains(characterName))
					return character;
			}
			return null;
		}

		// Check if an Asset has a given target
		private Asset AssetIsTarget

		// Handles the parsing of input from the user.
		public void ParseInput(string prompt)
		{
			string input = GetInput(prompt).ToLower().Trim();

			if (IsValidInput(input) && (input != "") && input != null)
			{
				string verb = "";
				string noun = "";

				// split the incoming string and check it against the possible verbs in the parseDict.
				string[] parsed = input.Split(default(string[]), 2, StringSplitOptions.RemoveEmptyEntries);
				if (this.parseDict.ContainsKey(parsed[0]))
					verb = this.parseDict[parsed[0]];
				
				if (parsed.Length == 0)
				{
					commands.TypeLine($"Sorry, {this.Player.PlayerName} I didn't catch that.");
					return;
				}
				else if (parsed.Length == 1)
				{
					parsed = new string[2] { verb, "none" };
				}
				else
				{
					noun = parsed[1];
				}

				if (allowedVerbs.Contains(verb))
				{
					// Needs logic on how to use each verb.
					switch (verb)
					{
						case "drop":
							Item itemToDrop = DoesItemExistIn(this.Player.Inventory, noun);
							this.commands.Drop(this.Player, itemToDrop);
							break;

						case "get":
							//Checks against the parseValues of items in current location.
							Item itemToGet = DoesItemExistIn(this.Player.PlayerLocation.Items, noun);
							if (itemToGet == null)
							{
								WriteLine($"You're not able to get {noun}.");
								break;
							}
							this.commands.Get(this.Player, itemToGet);
							break;

						case "give":
							// Get the target character for it item being given.
							string giveResponse= GetInput($"Who do you want to give {noun}?");

							// check that item is in player inventory
							Item itemToGive = DoesItemExistIn(this.Player.Inventory, noun);

							// check characters.parsevalue is in location (write method to check location) IsInLocation
							Character targetCharacter = CharacterExistsIn(this.Player.PlayerLocation, giveResponse);

							// pass player item being gotten (Item), character(Character), character dictionary
							commands.Give(this.Player, itemToGive, targetCharacter, this.Player.PlayerLocation.Characters);
							break;

						case "go":
							commands.Go(Player, noun, this.Locations);
							break;

						case "inventory":
							commands.Inventory(Player);
							break;

						case "look":
							commands.Look(Player, noun);
							break;

						case "put":
							//handle like use
							string putResponse = GetInput($"Where do you want to put {noun}?");
							commands.Put(Player, noun, items);
							break;

						case "talk":
							// check player.location.characters.parseValues for match, pass character.
							commands.Talk(Player, parsed[1], Player.PlayerLocation.Characters);
							break;

						case "use":
							// Get the target for the use command
							string useTarget = GetInput($"What do you want to use {noun} on?");

							//check player inventory items parseValue vs noun
							Item itemToUse = DoesItemExistIn(this.Player.Inventory, noun);
							string itemTarget = itemToUse.Target;

							//check player inventory items.parseValue for target AND current location characters for parsevalues
							// pass player, item, asset
							commands.Use(Player, parsed[1], useTarget);
							break;

						case "help":
							commands.Help(Player);
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
					WriteLine("I'm sorry, I didn't understand that. For information about what kinds of commands are available, type \"help\".");
				}
				if (parsed[0] != "look" && parsed[0] != "quit" && parsed[0] != "go")
					WriteLine($"{this.Player.PlayerLocation.DescriptionShort}");
			}
			else
			{
				WriteLine("I'm sorry, I didn't understand that. For information about what kinds of commands are available, type \"help\".");
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
			WriteLine(commands.WrapText("Where is Bigfoot is a text-based adventure game where you take on the role of a camper who is trying to find that most elusive of cryptids: BIGFOOT!"));
			WriteLine("");
			WriteLine("First, you need to decide who you are... ");
			//string startGame = GetInput("Would you like to start the game? (y/n): ");
			//if (startGame == "yes" || startGame == "y") {
			//    return;
			//    }
			//else if (startGame == "no" || startGame == "n")
			//{
			//    WriteLine("See you later!");

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
			} while (!IsValidInfo(name) && name != "");

			do
			{
				gender = GetInput("What gender are you? ");
			} while (!IsValidInfo(gender) && gender != "");

			do
			{
				hair = GetInput("What color is your hair? ");
			} while (!IsValidInfo(hair) && hair != "");

			string[] deets = { name, gender, hair };

			commands.TypeLine("Now that that's done with...");
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

			game.commands.TypeLine(game.commands.WrapText("Your old buddy, Dan, from college was always crazy about finding Bigfoot. You even went on a couple of Bigfoot hunting expeditions with him way back when. But you weren't expecting him to contact you out of the blue and invite you on another one, now that it's been years since you graduated. But the memory of how relaxing those previous trips were made you agree to go along.\n\nYou drove out from Seattle last night, and into the wilderness between Mount Rainier and Mount St. Helens. You set up your camp near the area where Dan said he'd been camping (you hope--the directions weren't exactly great), and crashed for the night. Now it's morning. Time to find your old buddy."));

			game.commands.ShowLocation(game.Player.PlayerLocation);

			do
			{
				game.ParseInput("What would you like to do?\n> ");

			} while (game.running == true);
		}

		// Check against a regex string that allows all letters, spaces, apostrophes, dashes.
		public static bool IsValidInput(string str)
		{
			Regex regexString = new Regex(@"^[a-zA-Z\s-']+\z");
			return regexString.IsMatch(str);
		}

		// check against a regex string that takes one word of just letters, or two words separated by a space.
		// Format is beginning of line, one or more letters of any length, one or no spaces, zero or more letters of any length, end of line.
		public static bool IsValidInfo(string str)
		{
			Regex regexString = new Regex(@"^[a-zA-Z]+\s?[a-zA-Z]*\z");
			return regexString.IsMatch(str);
		}

		// Take in string input from the user. Corrects null input.
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
