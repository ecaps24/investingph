using investingph.Converters;
using investingph.Data;
using investingph.Models;
using investingph.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace investingph.Services
{
    public class InitializeService
    {

        public InitializeService()
        {
            Initialize();
        }
        public async void Initialize()
        {
            MarketTimeConverter market = new MarketTimeConverter();
            LocalStorageService storage = new LocalStorageService();
            DateTime LastWrite = DateTime.UtcNow.AddHours(8);
            var exists = await storage.FileExists();
            StockServices services = new StockServices();
            string jsonStocks = "";
            if (exists)
            {
                try
                {
                    jsonStocks = await services.GetStringJson();
                    var root = JsonConvert.DeserializeObject<StockRoot>(jsonStocks);
                    var LastServerUpdate = root.LastUpdate;
                    LastWrite = await storage.GetLastWrite();
                    if (LastServerUpdate > LastWrite) //check if data server is updated
                    {
                        await storage.WriteText(jsonStocks);
                    }
                    
                }
                catch (Exception)
                {

                    return;
                }


            }
            else //write the file automatically
            {
                jsonStocks = await services.GetStringJson();
                var root = JsonConvert.DeserializeObject<StockRoot>(jsonStocks);
                await storage.WriteText(jsonStocks);
            }


        }

  
    }
}
