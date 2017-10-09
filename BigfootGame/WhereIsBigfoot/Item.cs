using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    public class Item : Asset
    {
        string name, title, descriptionFirst, descriptionShort, descriptionLong;
		List<string> parseValue;
		Dictionary<string, string> actions;
        string location;
        string target;

        public Item(string name, 
                    string title,
                    List<string> parseValue, 
                    string descriptionFirst, 
                    string descriptionShort, 
                    string descriptionLong,
                    string target, Dictionary<string, string> actions,
                    string location) : base(name,
                                            title,
											parseValue,
                                            descriptionFirst, 
                                            descriptionShort, 
                                            descriptionLong)
        {
            this.name = name;
            this.title = title;
			this.parseValue = parseValue;
            this.descriptionFirst = descriptionFirst;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.actions = actions;
            this.target = target;
            this.location = location;
        }

        public Dictionary<string, string> Actions
        {
            get => this.actions;
            set => this.actions = value;
        }

        public string Location {
            get => this.location;
            set => this.location = value;
        }

        public string Target
        {
            get => this.target;
            set => this.target = value;
        }
    }
}
