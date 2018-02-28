using investingph.Models;
using investingph.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using Plugin.Connectivity;
using System.Linq;
using investingph.Helpers;
using PCLStorage;
using System.Threading.Tasks;
using investingph.Info;

namespace investingph.ViewModels
{
    public class StockListViewModel : BaseViewModel
    {

        StockServices stockServices = new StockServices();

        private Command<string> _refreshCommand;
        private ObservableCollection<Stock> stockList;
        public ObservableCollection<Stock> StockList
        {
            get { return stockList; }
            set
            {
                stockList = value;
                OnPropertyChanged("StockList");
            }
        }


        public StockListViewModel()
        {

            _refreshCommand = new Command<string>(RefreshListView);
            Title = "Stocks";
            StatusMessage = "This is a test...";
            RefreshListView();
            IsBusy = false;
        }

        public async void RefreshListView(string parameters = "")
        {
            IsBusy = true;
            bool status = CrossConnectivity.Current.IsConnected;
            IsConnected = status ?
                "Status: Connected" : "Status: No Connection";
            ObservableCollection<Stock> list = new ObservableCollection<Stock>();
            list = new ObservableCollection<Stock>
                (await stockServices.GetStockList(parameters));

            StockList = list;
            IsBusy = false;

        }


        public Command<string> RefreshCommand
        {
            get
            {
                return _refreshCommand;
            }
        }

        private Command<Object> _addToWatchListCommand;

        public Command<Object> AddToWatchListCommand
        {
            get
            {
                return _addToWatchListCommand;
            }
            set
            {
                _addToWatchListCommand = value;
            }
        }

        public async Task AddToWatchList(string symbol)
        {
            var watch = new WatchList() { Symbol = symbol };
            await App._WatchListData.AddNewStockAsync(watch);
            StatusMessage = App._WatchListData.StatusMessage;
        }

        public async Task AddToPortfolio(Portfolio port)
        {
            await App._PortfolioData.AddNewPortfolioAsync(port);
            StatusMessage = App._PortfolioData.StatusMessage;

        }

        public async Task PreparePortfolio(string symbol)
        {
           // Stock s  = await stockServices.Stock(symbol);

        }

        public async Task UpdatePortfolio(Portfolio port)
        {
            await App._PortfolioData.UpdatePortfolioAsync(port);
            StatusMessage = App._PortfolioData.StatusMessage;


        }

    }
}
