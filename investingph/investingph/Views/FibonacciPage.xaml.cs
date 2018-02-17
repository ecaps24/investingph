using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace investingph.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FibonacciPage : ContentPage
	{

        FibonacciViewModel fvm = new FibonacciViewModel();

        public FibonacciPage ()
		{
			InitializeComponent ();
		}


        protected override void OnDisappearing()
        {
            Save();
        }

        async Task Save()
        {

            App.Current.Properties["HighPrice"] = EntryHighPrice.Text;
            App.Current.Properties["LowPrice"] = EntryLowPrice.Text;
            App.Current.Properties["Custom"] = Custom.Text;
            await App.Current.SavePropertiesAsync();
        }
        protected override void OnAppearing()
        {
            try
            {
                EntryHighPrice.Text = App.Current.Properties["HighPrice"].ToString();
                EntryLowPrice.Text = App.Current.Properties["LowPrice"].ToString();
                Custom.Text = App.Current.Properties["Custom"].ToString();

            }
            catch (Exception e)
            {
                EntryHighPrice.Text = "";
                EntryLowPrice.Text = "";

                Custom.Text = "";
            }
        }



    }
}