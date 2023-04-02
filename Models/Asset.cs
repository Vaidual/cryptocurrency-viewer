using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class Asset
    {
        public string id { get; set; }
        public int rank { get; set; }
        public string name { get; set; }
        public decimal? marketCapUsd { get; set; }
        public decimal? vwap24Hr { get; set; }
        public decimal priceUsd { get; set; }
        public decimal? supply { get; set; }
        public decimal? volumeUsd24Hr { get; set; }
        public decimal? changePercent24Hr { get; set; }
    }
}
