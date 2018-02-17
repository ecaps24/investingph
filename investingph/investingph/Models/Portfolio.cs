using investingph.Services;
using SQLite;
using System;


namespace investingph.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        [PrimaryKey, AutoIncrement]
        public int PortfolioID { get; set; }

        [Unique, MaxLength(10), NotNull]
        public string Symbol { get; set; }

        public string Company { get; set; }

        public double AveragePrice { get; set; }

        public int TotalShares { get; set; }

        public DateTime AddDate { get; set; }


        private DateTime marketUpdateTime;

        public DateTime MarketUpdateTime
        {
            get { return marketUpdateTime; }
            set
            {
                marketUpdateTime = DateTime.Now;
            }
        }


        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                price = new StockServices().Stock(Symbol).Result.Price.Amount;

            }
        }



        public double NetMarketValue =>
            (TotalShares * Price) - ((TotalShares * Price) * 0.0083);

        public double OriginalMarketValue => TotalShares * AveragePrice;

        public double NetGain => NetMarketValue - OriginalMarketValue;

        public double PercentGain =>
            Math.Round((NetGain / OriginalMarketValue) * 100, 3);

        public double NetLossGain { get; set; }

        



    }
}
