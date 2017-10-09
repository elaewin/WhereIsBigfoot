using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsBigfoot
{
    public class Asset
    {
        string name, title, descriptionFirst, descriptionShort, descriptionLong;
		List<string> parseValue;

		public Asset() { }

        public Asset(string name, string title, List<string> parseValue, string descriptionFirst, string descriptionShort, string descriptionLong)
        {
            this.name = name;
            this.title = title;
			this.parseValue = parseValue;
            this.descriptionFirst = descriptionFirst;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        public string Title {
            get => this.title;
            set => this.title = value;
        }

		public List<string> ParseValue
		{
			get => this.parseValue;
			set => this.parseValue = value;
		}

		public string DescriptionFirst
        {
            get => this.descriptionFirst;
            set => this.descriptionFirst = value;
        }

        public string DescriptionShort
        {
            get => this.descriptionShort;
            set => this.descriptionShort = value;
        }

        public string DescriptionLong
        {
            get => this.descriptionLong;
            set => this.descriptionLong = value;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
