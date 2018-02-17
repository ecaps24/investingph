using investingph.Services;
using investingph.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using investingph.Models;

namespace investingph.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {

        public ICommand GoHomeCommand { get; set; }
        public ICommand GoPSECommand { get; set; }
        public ICommand GoCryptoCommand { get; set; }
        public ICommand GoTABCommand { get; set; }
        public ICommand GoSearchCommand { get; set; }
        public ICommand GoFibonacciCommand { get; set; }

        private List<Menu_> _menuList;

        public List<Menu_> MenuList
        {
            get {

                return _menuList; }
            set {
                _menuList = value;
                OnPropertyChanged();

            }
        }



        public MenuViewModel()
        {
            GoHomeCommand = new Command(GoHome);
            GoPSECommand = new Command(GoPSEPage);
            GoCryptoCommand = new Command(GoCryptoPage);
            GoTABCommand = new Command(GoTABPage);
            GoSearchCommand = new Command(GoSearchPage);
            GoFibonacciCommand = new Command(GoFibonacciPage);


            AddMenu();
        }


        void AddMenu()
        {
            MenuList = new List<Menu_>();
            MenuList.Add(new Menu_ { Item = "" });

            MenuList.Add(new Menu_ { ImgSource= "home.png", Item = "Home"} );
            MenuList.Add(new Menu_ { ImgSource = "stock.png", Item = "Stocks" });
            MenuList.Add(new Menu_ { ImgSource = "bitcoin.png", Item = "CryptoCurrency" });
            MenuList.Add(new Menu_ { ImgSource = "stocks.png", Item = "Fibonacci Calculator" });
            MenuList.Add(new Menu_ { ImgSource = "settings.png", Item = "Settings" });

            MenuList = MenuList;
        }

        void GoHome(object obj)
        {
            App.Locator.NavigationService.GoHome();
            App.MenuIsPresented = false;
    
        }

        void GoPSEPage(object obj)
        {
            App.Locator.NavigationService.NavigateTo(Locator.PSEPage);
            App.MenuIsPresented = false;

        }



        void GoSearchPage(object obj)
        {
            App.Locator.NavigationService.NavigateTo(Locator.SearchPage);
            App.MenuIsPresented = false;
            
        }

        void GoCryptoPage(object obj)
        {
            App.Locator.NavigationService.NavigateTo(Locator.CryptoPage);
            App.MenuIsPresented = false;
        }

        void GoFibonacciPage(object obj)
        {
            App.Locator.NavigationService.NavigateTo(Locator.FibonacciPage);
            App.MenuIsPresented = false;
        }

        void GoTABPage(object obj)
        {
            App.Locator.NavigationService.NavigateTo(Locator.TABPage);

            App.MenuIsPresented = false;
        }



    }
}
