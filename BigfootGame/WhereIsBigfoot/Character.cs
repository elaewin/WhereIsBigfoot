using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Character
    {
        string name, descriptionShort, descriptionLong;
        Dictionary<string, string> actions;
        string[] parseValue;

        public Character(string name, string descriptionShort, string descriptionLong, Dictionary<string, string> actions, string[] parseValue)
        {
            this.name = name;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.actions = actions;
            this.parseValue = parseValue;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string DescriptionShort
        {
            get { return this.name; }
        }

        public string DescriptionLong
        {
            get { return this.name; }
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
    }
}
