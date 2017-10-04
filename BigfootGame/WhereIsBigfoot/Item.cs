using System;
using System.Collections.Generic;
using System.Text;

namespace WhereIsBigfoot
{
    class Item : IAsset
    {
        string _name, _shortDescription, _longDescription;
        public Item(string name, string shortDescription, string longDescription) 
        {
            _name = name;
            _shortDescription = shortDescription;
            _longDescription = longDescription; 
        }

        public string Name{ get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FirstDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ShortDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LongDescription { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
