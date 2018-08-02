using investingph.Converters;
using investingph.Info;
using investingph.Models;
using investingph.Services;
using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace investingph.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StockListPage : ContentPage
	{
        Instructions ins = new Instructions();

		public StockListPage ()
		{
			InitializeComponent ();
            StartTimer(60, "Refresh");

            StartTimer(10);
            
        }

        protected override void OnAppearing()
        {
            switchAutoUpdate.IsToggled = true;

        }

        protected override void OnDisappearing()
        {
            switchAutoUpdate.IsToggled = false;
        }


        public void StartTimer(int seconds, string action="")
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
             {
                 if (action == "Refresh")
                 {
                     if (switchAutoUpdate.IsToggled == true)
                     {
                         Device.BeginInvokeOnMainThread(RefreshListAsync);
                     }
                 }
                 else
                 {
                     Device.BeginInvokeOnMainThread(ShowTips);

                 }
                 return true;
             });
        }



        private void ShowTips()
        {
         //   var vm = new StockListViewModel();
            string tip = ins.GetTips().ToString();
            ShowlblStatus(tip);

        }

        private async void ShowlblStatus(string text = "")
        {
            lblLayout.IsVisible = true;
            lblStatus.IsVisible = true;
            lblStatus.Text = text;
            //lblLayout.BackgroundColor = Color.Black;
            await lblStatus.ScaleTo(0.8, 100, Easing.SpringIn);
        }

        public void RefreshList()
        {
            RefreshListAsync();
        }

        public async void RefreshListAsync()
        {
            MarketTimeConverter market = new MarketTimeConverter();
            var active =await market.MarketActive();
            if (!active || IsBusy) return;
            {
                try
                {
                    lstView.BeginRefresh();

                }
                catch (Exception e)
                {

                    return;
                }
            }
            lstView.EndRefresh();
            
        }


        public async Task RefreshItemSource(string param = "")
        {
            StockListViewModel svm = BindingContext as StockListViewModel;


            StockServices services = new StockServices();

            //svm.RefreshCommand.Execute(null);

            //svm.RefreshListView(param);
            //if ((svm != null) && (svm.RefreshCommand.CanExecute(param)))
            //{
            //    svm.RefreshCommand.Execute(param);
            //}

            ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();


            var s= lstView.ItemsSource;
       
             
            //lstView.ItemsSource =
            //    stocks.OrderByDescending(s => s.PercentChange);
            //lstView.RefreshCommand.Execute(param);
            

       //     txtSort.Text = param;
        }

       



        async Task AddWatchList_Clicked(object sender, EventArgs e)
        {
            var vm = BindingContext as StockListViewModel;
            var stock = ((MenuItem)sender).CommandParameter as Stock;
            await vm.AddToWatchList(stock.Symbol);
            await DisplayAlert(vm.Title, vm.StatusMessage, "Ok");
        }

        async Task AddPortfolio_Clicked(object sender, EventArgs e)
        {
            BaseViewModel bvm = new BaseViewModel() { Title = "Add Holdings" };

            var stock = ((MenuItem)sender).CommandParameter as Stock;
            var p = new Portfolio()
            {
                Symbol = stock.Symbol,
                Company = stock.Name
            };
            await App.Locator.NavigationService
                .NavigateTo(Locator.PortfolioAddEditPage,p,bvm);

        }


        







    }
}



