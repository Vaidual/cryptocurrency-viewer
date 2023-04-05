using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class AssetPriceHistory
    {
        public double PriceUsd { get; set; }
        public long UnixTimestamp { get; set; }

        public AssetPriceHistory(double priceUsd, long unixTimestamp)
        {
            PriceUsd = priceUsd;
            UnixTimestamp = unixTimestamp;
        }
    }
}
