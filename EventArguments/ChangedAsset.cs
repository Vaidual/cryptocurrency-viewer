using cryptocurrency_viewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.EventArguments
{
    class ChangedAsset
    {
        public string Id { get; set; }
        public bool IsPriceHigher { get; set; }

        public ChangedAsset(string id, bool isPriceHigher)
        {
            Id = id;
            IsPriceHigher = isPriceHigher;
        }
    }
}
