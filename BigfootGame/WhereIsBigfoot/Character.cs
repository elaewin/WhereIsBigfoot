using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Character : Asset
    {
        string name, descriptionFirst, descriptionShort, descriptionLong;
        string[] parseValue;
        Dictionary<string, string> actions;
        string location;

        public Character(string name, 
                         string descriptionFirst, 
                         string descriptionShort, 
                         string descriptionLong, 
                         Dictionary<string, string> actions, 
                         string[] parseValue,
                         string location) : base(name, 
                                                 descriptionFirst, 
                                                 descriptionShort, 
                                                 descriptionLong)
        {
            this.name = name;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.actions = actions;
            this.parseValue = parseValue;
            this.location = location;
        }

        public Dictionary<string, string> Actions
        {
            get { return this.actions; }
            set { this.actions = value; }
        }

        public string[] ParseValue
        {
            get { return this.parseValue; }
        }

        public string CharacterName
        {
            get { return this.name; }
        }

        public string Location {
            get => this.location;
            set => this.location = value;
        }
    }
}
