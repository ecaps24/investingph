using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using investingph.Models;

using investingph.Services;

namespace investingph.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuPage : ContentPage
	{
		public MenuPage ()
		{
            BindingContext = new MenuViewModel();
            var settings = new ToolbarItem
            {
               
                Text = "Home",
                
            };
            this.ToolbarItems.Add(settings);
     
            InitializeComponent ();
		}

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ((ListView)sender).SelectedItem = null;
            }
        }

        private async Task OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var i = e.Item as Menu_;
            string val = i.Item.ToString();

            //if (i.Item == "Stocks")
            //    await App.Locator.NavigationService.NavigateTo(Locator.PSEPage);

            if (i.Item == "Home")
                await App.Locator.NavigationService.GoHome();


            if (i.Item == "CryptoCurrency")
                await App.Locator.NavigationService.NavigateTo(Locator.CryptoPage);
            
            if(i.Item=="Fibonacci Calculator")
                await App.Locator.NavigationService.NavigateTo(Locator.FibonacciPage);

            if (i.Item == "Calculator")
                await App.Locator.NavigationService.NavigateTo(Locator.CalculatorPage);


            App.MenuIsPresented = false;
    
        }
    }
}