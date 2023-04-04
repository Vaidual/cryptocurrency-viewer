using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal? MarketCapUsd { get; set; }
        public decimal? Vwap24Hr { get; set; }
        public decimal PriceUsd { get; set; }
        public decimal? Supply { get; set; }
        public decimal? VolumeUsd24Hr { get; set; }
        public decimal? ChangePercent24Hr { get; set; }
    }
}
