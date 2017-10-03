using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Location
    {
        //Dictionary<string, string> locationItems;
        //Dictionary<string, string> characters;
        string locationShort, locationLong, locationItems, characters, name;
        List<string> possibleDirections;

        public Location(string name, string shortDescription, string longDescription, string locationItems, string characters)
        {
            this.locationShort = shortDescription;
            this.locationLong = longDescription;

            this.locationItems = locationItems;
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
