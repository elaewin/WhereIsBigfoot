using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Location : Asset
    {
        string name, descriptionFirst, descriptionLong, descriptionShort;
        Dictionary<string, string> exits;
        Dictionary<string, Character> characters;
        string[] objects;
        Dictionary<string, Item> items;
        //bool visited = false;

        public Location(string name, 
                        string descriptionFirst, 
                        string descriptionLong, 
                        string descriptionShort, 
                        Dictionary<string, string> exits, 
                        Dictionary<string, Character> characters, 
                        string[] objects, Dictionary<string, Item> items) : base(name, 
                                                                                 descriptionFirst, 
                                                                                 descriptionShort, 
                                                                                 descriptionLong)
        {
            this.name = name;
            this.descriptionFirst = descriptionFirst;
            this.descriptionLong = descriptionLong;
            this.descriptionShort = descriptionShort;
            this.objects = objects;
            this.items = new Dictionary<string, Item>();
        }

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
    }
}

