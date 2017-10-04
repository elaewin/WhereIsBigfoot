using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereIsBigfoot
{
    interface IAsset
    {
        string Name { get; set; }
        string FirstDescription { get; set; }
        string ShortDescription { get; set; }
        string LongDescription { get; set; }
    }
}
