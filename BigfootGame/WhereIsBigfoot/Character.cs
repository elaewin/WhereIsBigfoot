using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    public class Character
    {
        string name, descriptionShort, descriptionLong, flavorText1, flavorText2, target;
        Dictionary<string, string> actions;

        public Character(string name, string descriptionShort, string descriptionLong, string flavorText1, string flavorText2, string target, Dictionary<string, string> actions)
        {
            this.name = name;
            this.descriptionShort = descriptionShort;
            this.descriptionLong = descriptionLong;
            this.flavorText1 = flavorText1;
            this.flavorText2 = flavorText2;
            this.target = target;
            this.actions = actions;
        }
    }
}
