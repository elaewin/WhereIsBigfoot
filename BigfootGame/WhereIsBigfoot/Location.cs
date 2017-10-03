using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Location
    {
        //Dictionary<string, string> locationItems;
        //Dictionary<string, string> characters;

        string locationName, locationShort, locationLong, characters, items;
        string[] objects;
        Dictionary<string, string> exits;

        public Location(string locationName, string locationShort, string locationLong, string items, string[] objects, string characters)
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

        public string Items 
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


        //public Dictionary<string, string> LocationItems
        //{
        //    get
        //    {
        //        return this.locationItems;
        //    }
        //    set
        //    {
        //        this.locationItems = value;
        //    }
        //}

        //public Dictionary<string, string> Characters
        //{
        //    get
        //    {
        //        return this.characters;
        //    }
        //    set
        //    {
        //        this.characters = value;
        //    }
        //}
    }
}
