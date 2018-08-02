using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace investingph.Views.Calculators
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RiskRewardPage : ContentPage
	{
		public RiskRewardPage ()
		{
			InitializeComponent ();
		}

        public void OnSharesChanged()
        {
            //var shares = Convert.ToDouble(entryShares.Text);
            //var capital = Convert.ToDouble(entryCapital.Text);
            //var entry = Convert.ToDouble(PriceEntry.Text);
            //entryCapital.Text = (shares * entry).ToString();
        }
	}
}