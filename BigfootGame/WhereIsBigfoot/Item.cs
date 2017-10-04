using System;
using System.Collections.Generic;
using System.Text;

namespace BigfootGame
{
    class Item
    {
        string itemName, itemShort, itemLong;

        public Item() {}

        public Item(string itemName, string itemShort, string itemLong)
        {
            this.itemName = itemName;
            this.itemShort = itemShort;
            this.itemLong = itemLong; 
        }

        public string ItemName
        {
            get { return this.itemName; }
        }

        public string ItemShort
        {
            get { return this.itemShort; }
        }

        public string ItemLong
        {
            get { return this.itemLong; }
        }
    }
}
