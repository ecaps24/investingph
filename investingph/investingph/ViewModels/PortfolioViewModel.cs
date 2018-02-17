using investingph.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using investingph.Converters;
using System.Threading.Tasks;
using investingph.Services;
using System.Linq;

namespace investingph.ViewModels
{
    public class PortfolioViewModel : BaseViewModel
    {
        #region Declarations
        private Command<string> refreshPortfolioCommand;
        public DateTime LastUpdated { get; set; }

        private double stockPrice;

        public double StockPrice
        {
            get { return stockPrice; }
            set
            {
                stockPrice = value;
                OnPropertyChanged("StockPrice");
            }
        }

        private double percentGainLoss;

        public double PercentGainLoss
        {
            get { return percentGainLoss; }
            set
            {
                percentGainLoss = value;
                OnPropertyChanged();
            }
        }


        private DateTime asOf;

        public DateTime AsOf
        {
            get { return asOf; }
            set
            {
                asOf = value;
                OnPropertyChanged("AsOf");
            }
        }


        private double originalValue;

        public double OriginalValue
        {
            get { return originalValue; }
            set
            {
                originalValue = value;
                OnPropertyChanged();
            }
        }


        private double gainLoss;

        public double GainLoss
        {
            get { return gainLoss; }
            set
            {
                gainLoss = value;
                OnPropertyChanged();
            }
        }


        private double netTotal;

        public double NetTotal
        {
            get { return netTotal; }
            set
            {
                netTotal = value;
                OnPropertyChanged();
            }

        }




        private ObservableCollection<Portfolio> portfolioList;

        public ObservableCollection<Portfolio> PortfolioList
        {
            get { return portfolioList; }
            set
            {
                portfolioList = value;
                OnPropertyChanged("PortfolioList");
            }
        }

        #endregion

        public PortfolioViewModel()
        {
            refreshPortfolioCommand = 
                new Command<string>(RefreshPortfolioList);
            Title = "Portfolio";
            RefreshPortfolioList();


            IsBusy = false;
        }

        private async void RefreshPortfolioList(string param = "")
        {
            StockServices services = new StockServices();
            IsBusy = false;
            ObservableCollection<Portfolio> port = new ObservableCollection<Portfolio>(
                await App._PortfolioData.GetPortfolioListAsync()); ;
            PortfolioList = port;

            NetTotal = PortfolioList.Sum(i => i.NetMarketValue);
            GainLoss = PortfolioList.Sum(i => i.NetGain);
            OriginalValue = PortfolioList.Sum(i => i.OriginalMarketValue);
            PercentGainLoss = Math.Round((GainLoss / OriginalValue) * 100, 2);
            AsOf = new MarketTimeConverter().ManilaTime;
            IsBusy = false;
        }

        public async Task DeletePortfolio(Portfolio port)
        {
            await App._PortfolioData.DeletePortfolioItemAsync(port);
            StatusMessage = App._PortfolioData.StatusMessage;
        }

        public Command<string> RefreshPortfolioCommand
        {
            get
            {
                return refreshPortfolioCommand;
            }
   
        }

    }
}
