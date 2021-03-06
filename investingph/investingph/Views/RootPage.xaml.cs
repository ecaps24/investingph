﻿using investingph.ViewModels;
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
    public partial class RootPage : MasterDetailPage
    {
        RootViewModel root = new RootViewModel();
        public RootPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
        }

        async void SearchClicked(object sender, EventArgs e)
        {
            BaseViewModel _base = new BaseViewModel()
                 { Title = "Search" };
            await Navigation.PushModalAsync(new SearchPage(_base), false);

            //root.RemoveToolBar(AddToolBar);
            //root.RemoveToolBar(SearchToolBar);
        }

        async void SortClicked(object sender, EventArgs e)
        {
            string sort = await DisplayActionSheet(
                "", "",
                null, "Active", "Gainers", "Losers", "Volume");
            if (sort == "") return;
            StockListPage slp = new StockListPage();
            //StockListViewModel svm = new StockListViewModel();

            await slp.RefreshItemSource(sort);
            //svm.RefreshCommand.CanExecute(sort);
            //svm.RefreshCommand.Execute(sort);
        }


        async void AddClicked(object sender, EventArgs e)
        {
            //await DisplayActionSheet("", "",
            //    null, "Add Portfolio", "Add WatchList");
            //root.RemoveToolBar(AddToolBar);
            //root.RemoveToolBar(SearchToolBar);
        }




    }
}

