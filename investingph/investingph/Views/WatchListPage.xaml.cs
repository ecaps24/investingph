using investingph.Converters;
using investingph.Models;
using investingph.Services;
using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace investingph.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WatchListPage : ContentPage
	{
		public WatchListPage ()
		{
			InitializeComponent ();
            StartTimer(60, "Refresh");
        }

        protected override void OnAppearing()
        {
            switchAutoUpdate.IsToggled = true;

        }

        protected override void OnDisappearing()
        {
            switchAutoUpdate.IsToggled = false;
        }



        private void StartTimer(int seconds, string action = "")
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
            throw new NotImplementedException();
        }

        public async void RefreshListAsync()
        {
            MarketTimeConverter market = new MarketTimeConverter();
            var active = await market.MarketActive();
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



        private async Task DeleteStock_Clicked(object sender, EventArgs e)
        {

            lstView.BeginRefresh();
            var vm = BindingContext as WatchListViewModel;
            var stock = ((MenuItem)sender).CommandParameter as Stock;
            await vm.DeleteWatch(stock.Symbol);
            await DisplayAlert(vm.Title, vm.StatusMessage, "Ok");
            lstView.EndRefresh();

        }

        private async Task AddToPortfolio_Clicked(object sender, EventArgs e)
        {
            BaseViewModel bvm = new BaseViewModel() { Title = "Add Holdings" };

            var stock = ((MenuItem)sender).CommandParameter as Stock;
            var p = new Portfolio()
            {
                Symbol = stock.Symbol,
                Company = stock.Name
            };
            await App.Locator.NavigationService
                .NavigateTo(Locator.PortfolioAddEditPage, p, bvm);


        }
    }
}