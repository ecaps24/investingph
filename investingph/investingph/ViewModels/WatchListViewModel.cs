using investingph.Models;
using investingph.Services;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace investingph.ViewModels
{
    public class WatchListViewModel : StockListViewModel
    {
        private ObservableCollection<Stock> stockList;
        private ObservableCollection<WatchList> watchList;
        private List<string> _watch = new List<string>();

        private Command<string> _refreshWatchCommand;
        private string _statusMessage;




        public WatchListViewModel()
        {
            AddToWatchList("JFC");
            _refreshWatchCommand = new Command<string>(RefreshWatchList);
            Title = "Watch";
            IsBusy = true;
            RefreshWatchList();
       

        }

        public async void RefreshWatchList(string param = "")
        {
            IsBusy = false;
            bool status = CrossConnectivity.Current.IsConnected;
            IsConnected = status ? 
                "Status: Connected" : "Status: No Connection";

            try
            {
                StockServices stockServices = new StockServices();
                List<Stock> list = await stockServices.GetStockList();
                ObservableCollection<WatchList> watch = 
                    new ObservableCollection<WatchList>(
                    await App._WatchListData.GetWatchListAsync());
 
                List<string> ws = new List<string>();
                foreach (var item in watch)
                {
                    ws.Add(item.Symbol);
                }
                list = list.Where(s => ws.Contains(s.Symbol)).ToList();
                StockList = new ObservableCollection<Stock>(list);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
            IsBusy = false;

        }


        public async Task DeleteWatch(string symbol)
        {
            await App._WatchListData.DeleteStockAsync(symbol);
            StatusMessage = App._WatchListData.StatusMessage;

        }

        public async Task<int> GetWatchList()
        {
            await Task.Delay(200);
            ObservableCollection<WatchList> list = new ObservableCollection<WatchList>(
                await App._WatchListData.GetWatchListAsync());
            return list.Count();

        }

        

        public Command<string> RefreshWatchCommand
        {
            get
            {
                return _refreshWatchCommand;
            }
        }


    }
}
