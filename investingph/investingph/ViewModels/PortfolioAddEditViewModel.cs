using investingph.Models;
using investingph.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace investingph.ViewModels
{
    public class PortfolioAddEditViewModel : StockListViewModel
    {

        private Stock stock;

        public Stock Stock
        {
            get { return stock; }
            set { stock = value;
                OnPropertyChanged("Stock");

            }
        }

        public PortfolioAddEditViewModel()
        {
            Stock = Stock;
        }

        public async Task UpdatePortfolio(Portfolio port)
        {
            await App._PortfolioData.UpdatePortfolioAsync(port);
            StatusMessage = App._PortfolioData.StatusMessage;
        }


    }
}
