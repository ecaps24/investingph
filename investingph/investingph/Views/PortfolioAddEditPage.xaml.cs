using investingph.Calculations;
using investingph.Models;
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
    public partial class PortfolioAddEditPage : ContentPage
    {


        public PortfolioAddEditPage(Portfolio port,  BaseViewModel bvm)
        {

            InitializeComponent();
            var vm = BindingContext
            as PortfolioAddEditViewModel;
            lblSymbol.Text = port.Symbol;
            lblCompany.Text = port.Company;
            SharesEntry.Text = port.TotalShares == 0 ? "" : port.TotalShares.ToString();
            EntryAvgPrice.Text = port.AveragePrice==0 ? "" : port.AveragePrice.ToString();
            Title = bvm.Title;

        }

        protected override void OnAppearing()
        {
            SharesEntry.Focus();
        }



        async Task AddPosition_Clicked()
        {

            var vm = BindingContext as PortfolioAddEditViewModel;
            var edit = Title=="Edit Portfolio" ? true : false;
            var avg = Convert.ToDouble(EntryAvgPrice.Text);
            var shares = Convert.ToDouble(SharesEntry.Text);
            if (avg==0 || shares == 0)
            {
                await DisplayAlert("investing.ph", "Total Shares or Average Price" +
                    " cannot be blank.", "OK");
                return;
            }

            Portfolio p = await App._PortfolioData._Portfolio(lblSymbol.Text);
            var exists = p == null ? false : true;

            if (exists || edit)
            {
                var newAVG = new WeightedAverage().AverageWeightedPrice(
                    p.TotalShares, p.AveragePrice, shares, avg);
                var newShares = shares + p.TotalShares;
                var port = new Portfolio()
                {
                    Symbol = lblSymbol.Text,
                    Company = lblCompany.Text,
                    TotalShares = edit ? shares : newShares,
                    AveragePrice = edit ? avg : newAVG,
                    PortfolioID = p.PortfolioID

                };

                await vm.UpdatePortfolio(port);
            }
            else
            {
                var port = new Portfolio()
                {
                    Symbol = lblSymbol.Text,
                    Company = lblCompany.Text,
                    AveragePrice = avg,
                    TotalShares = shares
                };
                await vm.AddToPortfolio(port);
            }
 
           await DisplayAlert(vm.Title, vm.StatusMessage, "Ok");
           await App.Locator.NavigationService.GoHome();
            
        }



    }
}