using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    internal class Asset
    {
        public string id { get; set; }
        public int rank { get; set; }
        public string name { get; set; }
        public double? marketCapUsd { get; set; }
        public double? vwap24Hr { get; set; }
        public double? supply { get; set; }
        public double? volumeUsd24Hr { get; set; }
        public double? changePercent24Hr { get; set; }
    }
}
