﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Location
    {
        string locationName, locationShort, locationLong;
        string[] objects;
        Dictionary<string, string> exits;
        Dictionary<string, Item> items;
        Dictionary<string, Character> characters;

        public Location(string locationName, string locationShort, string locationLong, Dictionary<string, Item> items, string objects, string characters)

        {
            this.locationName = locationName;
            this.locationShort = locationShort;
            this.locationLong = locationLong;

            this.items = items;
            this.objects = objects;
            this.characters = characters; 
        }

        public string LocationShort
        {
            get { return this.locationShort; }
        }

        public string LocationLong
        {
            get{ return this.locationLong; }
        }

        public string LocationName 
        { 
            get{ return this.locationName; }
        }

        public Dictionary<string, Item> Items 
        { 
            get{ return this.items; }
        }

        public string [] Objects 
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
