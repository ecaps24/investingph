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

        public void RefreshListAsync()
        {
            MarketTimeConverter market = new MarketTimeConverter();
           
            if (!market.MarketActive || IsBusy) return;
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

            //ObservableCollection<Stock> stocks = new ObservableCollection<Stock>();
            //stocks = new ObservableCollection<Stock>(
            //    await services.GetStockList(param));
            svm.RefreshListView(param);
            if ((svm != null) && (svm.RefreshCommand.CanExecute(param)))
            {
                svm.RefreshCommand.Execute(param);
            }

            //lstView.BeginRefresh();
            //lstView.ItemsSource = stocks;
            //lstView.EndRefresh();

        }

        async void Gainers_ButtonClicked(object sender, EventArgs e)
        {
            await Gainers_Button.ScaleTo(1.25, 100, Easing.BounceOut);
            await Task.Delay(200);
            await Gainers_Button.ScaleTo(1, 100, Easing.BounceOut);
        }

        async void Active_ButtonClicked(object sender, EventArgs e)
        {
            await Active_Button.ScaleTo(1.25, 100, Easing.BounceOut);
            await Task.Delay(200);
            await Active_Button.ScaleTo(1, 100, Easing.BounceIn);
        }

        async void Volume_ButtonClicked(object sender, EventArgs e)
        {
            await Volume_Button.ScaleTo(1.25, 100, Easing.BounceIn);
            await Task.Delay(200);
            await Volume_Button.ScaleTo(1, 100, Easing.BounceIn);

        }

        async void Sort_Clicked(object sender, EventArgs e)
        {
            int animationSpeed = 5;
            if (Active_Button.IsVisible==false)
            {
                Active_Button.IsVisible = true;
                await Active_Button.ScaleTo(1, 100, Easing.CubicOut);
                await Task.Delay(animationSpeed);
                Gainers_Button.IsVisible = true;
                await Gainers_Button.ScaleTo(1, 100, Easing.CubicOut);
                await Task.Delay(animationSpeed);
                Volume_Button.IsVisible = true;
                await Volume_Button.ScaleTo(1, 100, Easing.CubicOut);
                await Task.Delay(animationSpeed);
                return;
            }

            Volume_Button.IsVisible = false;
            await Task.Delay(animationSpeed);
            Gainers_Button.IsVisible = false;
            await Task.Delay(animationSpeed);
            Active_Button.IsVisible = false;
            await Task.Delay(animationSpeed);
            

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



