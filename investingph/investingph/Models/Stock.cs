using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Models
{
    [Table("Stocks")]
    public class StockRoot
    {
        [JsonProperty("stock")]
        public List<Stock> Stock { get; set; }

        [JsonProperty("as_of")]
        public DateTime LastUpdate { get; set; }
    }


    public class Stock
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("percent_change")]
        public float PercentChange { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        public double TradedValue => Volume * Price.Amount;

        public double Change => Price.Amount * (PercentChange / 100);

        public DateTime LastUpdated { get; set; }


    }

    public class Price
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }
    }
}
