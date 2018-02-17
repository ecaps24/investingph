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
    public partial class PortfolioPage : ContentPage
    {
        PortfolioViewModel viewModel = new PortfolioViewModel();

        public PortfolioPage()
        {
            InitializeComponent();
            StartTimer(60, "Refresh");
        }

        protected override void OnAppearing()
        {
            switchAutoUpdate.IsToggled = true;
            RefreshListAsync();
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

                    Device.BeginInvokeOnMainThread(RefreshListAsync);

                }
                else
                {
                    Device.BeginInvokeOnMainThread(ShowTips);

                }
                return true;
            });

        }

        private void RefreshListAsync()
        {
            MarketTimeConverter market = new MarketTimeConverter();
            if (!market.MarketActive || IsBusy) return;

            try
            {
                lstPortfolio.BeginRefresh();
            }
            catch (Exception e)
            {

                return;
            }

            lstPortfolio.EndRefresh();

        }

        private void ShowTips()
        {
            throw new NotImplementedException();
        }

        async Task DeletePortfolio_Clicked(object sender, EventArgs e)
        {

            var p = ((MenuItem)sender).CommandParameter as Portfolio;
            await viewModel.DeletePortfolio(p);
            lstPortfolio.BeginRefresh();
            await DisplayAlert(viewModel.Title, viewModel.StatusMessage, "Ok");

        }




        async Task UpdatePortfolio_Clicked(object sender, EventArgs e)
        {
            BaseViewModel bvm = new BaseViewModel() { Title = "Edit Portfolio" };
            var p = ((MenuItem)sender).CommandParameter as Portfolio;
            var s = new Stock()
            {
                Symbol = p.Symbol,
                Name = p.Company
            };
            await App.Locator.NavigationService
                .NavigateTo(Locator.PortfolioAddEditPage, p, bvm);

        }
    }


}