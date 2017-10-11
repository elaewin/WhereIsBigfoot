using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WhereIsBigfoot
{
    public class Location : Asset
    {
        string name, title, descriptionFirst, descriptionLong, descriptionShort;
        Dictionary<string, string> exits;
        Dictionary<string, Character> characters;
        string[] objects;
        Dictionary<string, Item> items;
        bool visited = false;

        // create exits dictionary in location

        public Location(string name,
                        string title,
                        string descriptionFirst,
                        string descriptionLong,
                        string descriptionShort,
                        Dictionary<string, string> exits,
                        Dictionary<string, Character> characters,
                        string[] objects,
                        Dictionary<string, Item> items) : base(name,
                                                               title,
                                                               null,
                                                               descriptionFirst,
                                                               descriptionShort,
                                                               descriptionLong)
        {
            this.name = name;
            this.title = title;
            this.descriptionFirst = descriptionFirst;
            this.descriptionLong = descriptionLong;
            this.descriptionShort = descriptionShort;
            this.objects = objects;
            this.exits = exits;
            this.items = new Dictionary<string, Item>();
            this.characters = new Dictionary<string, Character>();
        }

        //[JsonConverter(typeof(Dictionary<string, string>))]
        public Dictionary<string, string> Exits
        {
            get => this.exits;
            set => this.exits = value;
        }

        public Dictionary<string, Character> Characters
        {
            get => this.characters;
            set => this.characters = value;
        }

        public string[] Objects
        {
            get => this.objects;
            set => this.objects = value;
        }

        public Dictionary<string, Item> Items
        {
            get => this.items;
            set => this.items = value;
        }

        public bool Visited
        {
            get => this.visited;
            set => this.visited = value;
        }
    }
}

