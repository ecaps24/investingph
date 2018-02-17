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
	public partial class PSEPage : ContentPage
	{
		public PSEPage ()
		{
			InitializeComponent ();
		}

        void Add(object sender, EventArgs e)
        {
            var s = slider.Value;
            slider.Value = ++slider.Value;
        }

        void Deduct(object sender, EventArgs e)
        {
            var s = slider.Value;
            slider.Value = --s;
        }
    }
}