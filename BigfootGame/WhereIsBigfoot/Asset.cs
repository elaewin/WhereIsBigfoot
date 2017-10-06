using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsBigfoot
{
    public class Asset
    {
        string name, descriptionFirst, descriptionShort, descriptionLong;

		public Asset() { }

        public Asset(string name, string descriptionFirst, string descriptionShort, string descriptionLong)
        {
            this.name = name;
            this.descriptionFirst = descriptionFirst;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
        }

        public string Name
        {
            get => this.name;
            set => this.name = value;
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
