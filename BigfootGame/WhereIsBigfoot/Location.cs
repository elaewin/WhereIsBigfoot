using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Location
    {
        string name, locationShort, locationLong;
        string[] objects;
        Dictionary<string, string> exits;
        Dictionary<string, Item> items;
        bool visited = false;
        Dictionary<string, Character> characters;

        public Location(string name, string locationShort, string locationLong, string[] objects)

        {
            this.name = name;
            this.locationShort = locationShort;
            this.locationLong = locationLong;
            this.objects = objects;
        }

        public string LocationShort
        {
            get { return this.locationShort; }
        }

        public string LocationLong
        {
            get{ return this.locationLong; }
        }

        public string Name 
        { 
            get{ return this.name; }
        }

        public Dictionary<string, Item> Items 
        { 
            get{ return this.items; }
        }

        public string[] Objects 
        { 
            get{ return this.objects; }
        }

        public Dictionary<string,string> Exits
        {
            get
            {
                return this.exits;
            }
            set
            {
                this.exits = value;
            }
        }

        public Dictionary<string, Character> Characters
        {
            get { return this.characters; }
            set { this.characters = value; }
        }
    }
}
