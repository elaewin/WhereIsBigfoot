using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    public class Character : Asset
    {
        string name, title, descriptionFirst, descriptionShort, descriptionLong;
        List<string> parseValue;
        Dictionary<string, string> actions;
        string location;

        public Character(string name, 
                         string title,
                         string descriptionFirst, 
                         string descriptionShort, 
                         string descriptionLong, 
                         Dictionary<string, string> actions,
                         List<string> parseValue,
                         string location) : base(name,
                                                 title,
                                                 descriptionFirst, 
                                                 descriptionShort, 
                                                 descriptionLong)
        {
            this.name = name;
            this.title = title;
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

        public List<string> ParseValue
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
