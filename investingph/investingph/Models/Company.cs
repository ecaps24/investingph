using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Models
{
   
    public class Company
    {
        [JsonProperty("Symbol")]
        public string Symbol { get; set; }
        [JsonProperty("Name")]
        public string CompanyName { get; set; }

        public bool IsVisible { get; set; }
    }

    




}
