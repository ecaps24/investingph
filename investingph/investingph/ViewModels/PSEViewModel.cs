using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using investingph.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace investingph.ViewModels
{

    public class PSEViewModel : BaseViewModel
    {
        public PSEViewModel()
        {
            Title = "Stocks";
        }
    }
    
}
