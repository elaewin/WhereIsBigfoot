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
        string DescriptionFirst { get; set; }
        string DescriptionShort { get; set; }
        string DescriptionLong { get; set; }
    }
}
