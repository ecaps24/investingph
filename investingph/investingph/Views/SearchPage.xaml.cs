using investingph.Converters;
using investingph.Data;
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
	public partial class SearchPage : ContentPage
	{
		public SearchPage(BaseViewModel bvm)
		{
			InitializeComponent ();

            Title = bvm.Title;
        }

        protected override void OnAppearing()
        {
            SearchStock.Focus();
            base.OnAppearing();
        }

        private async Task Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async Task SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            
           // SearchViewModel vm = new SearchViewModel();
            CompanyData cData = new CompanyData();
           // var c = SuggestionsListView.ItemsSource;
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                SuggestionsListView.ItemsSource = "";
            }

            else
            {
                ObservableCollection<Company> companies = 
                    new ObservableCollection<Company>();


                companies = new ObservableCollection<Company>
                    (await cData.GetCompanyList());

                //companies = new ObservableCollection<Company>
                //  ((SuggestionsListView.ItemsSource).ToList();
                var a = SuggestionsListView.ItemsSource 
                    as ObservableCollection<Company>;
                
                
                SuggestionsListView.ItemsSource =
                     companies.Where(c => c.CompanyName
                    .ToLower().Contains(e.NewTextValue.ToLower())
                    || (c.Symbol.ToLower().Contains(e.NewTextValue.ToLower())));

                
            }
        

        }

        private async Task AddToWatchList(object sender, ItemTappedEventArgs e)
        {
            BaseViewModel bvm = new BaseViewModel
            { Title = "Add Holdings" };

            var vm = BindingContext as SearchViewModel;
            var s = e.Item as Company;

            bool watch = Title.Contains("WatchList");
            bool port = Title.Contains("Portfolio");

            if (watch)
            {
                await vm.AddToWatchList(s.Symbol);
                await DisplayAlert(vm.Title, vm.StatusMessage, "Ok");
                await App.Locator.NavigationService.GoBack();

            }
            else if (port)
            {
                // await services.Stock(s.Symbol);
                var p = new Portfolio()
                {
                    Symbol = s.Symbol,
                    Company = s.CompanyName
                };

                await App.Locator.NavigationService
                    .NavigateTo(Locator.PortfolioAddEditPage, p, bvm);
            }

            else
            {
                //for now
                await vm.AddToWatchList(s.Symbol);
                await DisplayAlert(vm.Title, vm.StatusMessage, "Ok");
                await App.Locator.NavigationService.GoBack();
            }

            ((ListView)sender).SelectedItem = null;
        }
    }
}