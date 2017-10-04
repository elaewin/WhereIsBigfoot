using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Item : IAsset
    {
        string name, descriptionFirst, descriptionShort, descriptionLong;
        string[] parseValue;
        Dictionary<string, string> actions;

        public Item(string name, string descriptionShort, string descriptionLong, string[] parseValue, string target, Dictionary<string, string> actions) 
        {
            this.name = name;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.parseValue = parseValue;
            this.actions = actions;
        }

        public string Name { get => this.name; set => this.name = value; }

        public string DescriptionFirst { get => this.descriptionFirst; set => this.descriptionFirst = value; }

        public string DescriptionShort { get => this.descriptionShort; set => this.descriptionShort = value; }

        public string DescriptionLong { get => this.descriptionLong; set => this.descriptionLong = value; }

        public string[] ParseValue { get => this.parseValue; set => this.parseValue = value; }
    }
}
