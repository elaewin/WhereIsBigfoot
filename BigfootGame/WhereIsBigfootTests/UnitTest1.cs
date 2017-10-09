using System;
using WhereIsBigfoot;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace WhereIsBigfootTests
{
	[TestClass]
	public class UnitTest1
	{
		Game testGame = new Game();

		//// Test assignment of characters by count of the characters dict on the location.
		//TypeLine("Location Characters count:");
		//foreach (Location location in game.locations)
		//TypeLine($"Location: {location.Name}, # of items: {location.Characters.Count}");

		// Test existence of exits dict by count of the characters array on the location.
		//TypeLine("Location Exits count:");
		//foreach (Location location in game.locations)
		//TypeLine($"Location has {location.Exits.Count}");

		//// Test assignment of items by count of the items dict on the location.
		//TypeLine("Location Items count:");
		//foreach (Location location in game.locations)
		//TypeLine($"Location: {location.Name}, # of items: {location.Items.Count}");

		////Testing to make sure the objects are being de-serialized by writing them to the console.
		//TypeLine("Locations:");
		//foreach (Location location in game.locations)
		//    TypeLine(location.Name);

		//TypeLine("Items:");
		//foreach (Item item in game.Items) { 
		//    TypeLine($"Item {item.Name} is in location {item.Location}");
		//    foreach (string word in item.ParseValue)
		//        commands.TypeLine(word);
		//}
		//TypeLine("Characters:");
		//foreach (Character character in game.characters)
		//TypeLine(character.CharacterName);

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
