using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class Candle
    {
        public decimal Hight { get; set; }
        public decimal Low { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public int Period { get; set; }
    }
}
