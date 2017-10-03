using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
        //Examples of what would be in here for JSON
        //public string Email { get; set; }
        //public bool Active { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public IList<string> Roles { get; set; }

    class Location
    {
        Dictionary<string, string> locationItems;
        Dictionary<string, string> characters;
        string locationShort, locationLong;

        public Location(string shortDescription, string longDescription)
        {
            this.locationShort = shortDescription;
            this.locationLong = longDescription;
        }

        public string LocationShort
        {
            get { return this.locationShort; }
        }

        public string LocationLong
        {
            get{ return this.locationLong; }
        }

        public Dictionary<string, string> LocationItems
        {
            get
            {
                return this.locationItems;
            }
            set
            {
                this.locationItems = value;
            }
        }

        public Dictionary<string, string> Characters
        {
            get
            {
                return this.characters;
            }
            set
            {
                this.characters = value;
            }
        }
    }
}
