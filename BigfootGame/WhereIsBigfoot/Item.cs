using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Item : Asset
    {
        string name, descriptionFirst, descriptionShort, descriptionLong;
        string[] parseValue;
        Dictionary<string, string> actions;

        public Item(string name, string descriptionFirst, string descriptionShort, string descriptionLong, string[] parseValue, string target, Dictionary<string, string> actions) : base(name, descriptionFirst, descriptionShort, descriptionLong)
        {
            this.name = name;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.parseValue = parseValue;
            this.actions = actions;
        }

        public string[] ParseValue
        {
            get => this.parseValue;
            set => this.parseValue = value;
        }

        public Dictionary<string, string> Actions
        {
            get => this.actions;
            set => this.actions = value;
        }
    }
}
