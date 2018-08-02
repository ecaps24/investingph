using investingph.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using investingph.ViewModels;
using investingph.Views;

namespace investingph.ViewModels
{
    public class RootViewModel : BaseViewModel
    {
        private Command<string> addCommand;

        public Command<string> AddCommand
        {
            get { return addCommand; }

        }


        private Command<string> _searchCommand;
        public Command<string> SearchCommand
        {
            get
            {
                return _searchCommand;
            }
        }

        private Command<ToolbarItem> sortCommand;

        public Command<ToolbarItem> SortCommand
        {
            get { return sortCommand; }
            set { sortCommand = value; }
        }


        public RootViewModel()
        {
            _searchCommand = new Command<string>(ExecuteSearchCommand);
            addCommand = new Command<string>(ExecuteAddCommand);
            sortCommand = new Command<ToolbarItem>(ExecuteSortCommand);
        }

        private async void ExecuteSortCommand(ToolbarItem item)
        {

            string sort = await Application.Current.MainPage.DisplayActionSheet(
                "", "",
                null, "Active", "Gainers", "Losers", "Volume");
            if (sort == "") return;
            
          //  StockListViewModel svm = new StockListViewModel();
            StockListPage slp = new StockListPage();
            await slp.RefreshItemSource(sort);

        }

        private async void ExecuteSearchCommand(string param = "")
        {
            await App.Locator.NavigationService.NavigateTo(Locator.SearchPage);
            App.MenuIsPresented = false;
        }

        public void RemoveToolBar(ToolbarItem toolBar)
        {


            Application.Current.MainPage.ToolbarItems
                .Remove(toolBar);


        }

        public void AddToolBar(ToolbarItem toolBar)
        {
            Application.Current.MainPage.ToolbarItems
                .Add(toolBar);
        }

        public async void ExecuteAddCommand(string param = "")
        {
            BaseViewModel bvm = new BaseViewModel();
            string add = await Application.Current.MainPage.DisplayActionSheet(
                "", "",
                null, "Add Portfolio", "Add WatchList");
            //Application.Current.MainPage.ToolbarItems.Remove("AddToolBar");

            switch (add)
            {
                case "Add Portfolio":
                    bvm.Title = "Add Portfolio";
                    await App.Locator.NavigationService.NavigateTo(
                        Locator.SearchPage, bvm);
                    break;
                case "Add WatchList":
                    bvm.Title = "Add WatchList";

                    await App.Locator.NavigationService.NavigateTo(
                        Locator.SearchPage, bvm);
                    break;
                default:
                    break;

            }
        }

    }
}
