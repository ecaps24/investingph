using investingph.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using investingph.Data;
using investingph.ViewModels;

namespace investingph.Services
{
    public class StockServices : BaseViewModel
    {
        const string TickerUrl = "http://phisix-api3.appspot.com/stocks.json";
        const string TickerUrlStock = "http://phisix-api3.appspot.com/stocks/";
        string message = "";
        private ObservableCollection<Stock> stocks;

        public ObservableCollection<Stock> Stocks
        {
            get
            {
                
                return stocks;
            }
            set
            {
                stocks = value; ;
            }
        }


        public StockServices()
        {
     
        }

        HttpClient client = new HttpClient();
        public async Task<string> ServerResponse(string url = TickerUrl)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(TickerUrl);
                message = response.StatusCode.ToString();
            }
            catch (Exception e)
            {

                message = e.Message;
            }
            Debug.WriteLine(message);
            return message;
        }
        public async Task<string> GetStringJson()
        {
            try
            {
                var result = client.GetStringAsync(TickerUrl)
                 .GetAwaiter().GetResult();
                await Task.Delay(50);
                return result;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
                return "";
            }

        }

        public async Task<List<Stock>> GetLocalList()
        {
            List<Stock> stockList = new List<Stock>();
            LocalStorageService storage = new LocalStorageService();
            try
            {
                var result = await storage.ReadText();
                StockRoot stockRoot = JsonConvert
                    .DeserializeObject<StockRoot>(result);
                stockList = new List<Stock>(stockRoot.Stock);
                foreach (var item in stockList)
                {
                    item.LastUpdated = stockRoot.LastUpdate;
                }
            }
            catch (Exception e)
            {

                throw;
            }
            
            return stockList;
        }

        public async Task<List<Stock>> GetStockList(string parameters="")
        {
            StockData sData = new StockData();
            List<Stock> stockList = new List<Stock>();
            
            var message = await ServerResponse();

            if (message == "OK")
            {
                try
                {
                    var result = client.GetStringAsync(TickerUrl).
                      GetAwaiter().GetResult();
  
                    StockRoot stockRoot = JsonConvert
                        .DeserializeObject<StockRoot>(result);
                    stockList = new List<Stock>(stockRoot.Stock);
                    foreach (var item in stockList)
                    {
                        item.LastUpdated = stockRoot.LastUpdate;
                    }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    stockList=  await GetLocalList();
                }
            }
            else
            {
                stockList = await GetLocalList();
            }
            stockList = stockList.ToList();
            if (parameters == "Volume")
            {
                return stockList.OrderByDescending(s => s.Volume).ToList();
            }
            else if (parameters == "Gainers")
            {
                return stockList.OrderByDescending(s => s.PercentChange).ToList();
            }
            else
            {
                return stockList.OrderByDescending(s => s.TradedValue).ToList();

            }
        }

        public async Task<Stock> Stock(string symbol)
        {

            try
            {
                var url = $"{TickerUrlStock}{symbol}.json";
                var result = client.GetStringAsync(url)
                    .GetAwaiter().GetResult();
                StockRoot sr = JsonConvert
                    .DeserializeObject<StockRoot>(result);

                Stock s = new Stock();
                s = sr.Stock.FirstOrDefault();
                s.LastUpdated = sr.LastUpdate;
                return s;
            }
            catch (Exception)
            {
                var sList = await GetLocalList();
                var l = sList.Where(s => s.Symbol == symbol);
                return l.FirstOrDefault();
            }
        }

    }
}
