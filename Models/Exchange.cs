using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cryptocurrency_viewer.Models
{
    public class Exchange
    {
        public string Id { get; set; }

        [JsonProperty("exchangeUrl")]
        public string Url { get; set; }
    }
}
