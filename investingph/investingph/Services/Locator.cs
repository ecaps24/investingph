using GalaSoft.MvvmLight.Ioc;
using investingph.ViewModels;

using CommonServiceLocator;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using investingph.Views;
using investingph.Models;

namespace investingph.Services
{
   public class Locator
    {

        public const string RootPage = "RootPage";
        public const string MenuPage = "MenuPage";

        public const string HomePage = "HomePage";
        public const string PSEPage = "PSEPage";
        public const string CryptoPage = "CryptoPage";
        public const string SearchPage = "SearchPage";
        public const string PortfolioAddEditPage = "PortfolioAddEditPage";
        public const string FibonacciPage = "FibonacciPage";
        public const string CalculatorPage = "CalculatorPage";



        public const string TABPage = "TabbedPage1";

        static Locator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            var navigation = new NavigationService();
            //ViewModels

            navigation.Configure(RootPage, typeof(RootPage));
            navigation.Configure(MenuPage, typeof(MenuPage));

            navigation.Configure(HomePage, typeof(HomePage));
            navigation.Configure(PSEPage, typeof(PSEPage));
            navigation.Configure(CryptoPage, typeof(CryptoPage));
            navigation.Configure(SearchPage, typeof(SearchPage));
            navigation.Configure(PortfolioAddEditPage, typeof(PortfolioAddEditPage));
            navigation.Configure(FibonacciPage, typeof(FibonacciPage));
            navigation.Configure(CalculatorPage, typeof(CalculatorPage));


            navigation.Configure(TABPage, typeof(HomePage));


            SimpleIoc.Default.Register<RootViewModel>();
            SimpleIoc.Default.Register<MenuViewModel>();

            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<PSEViewModel>();
            SimpleIoc.Default.Register<CryptoViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<PortfolioAddEditViewModel>();
            SimpleIoc.Default.Register<FibonacciViewModel>();
            SimpleIoc.Default.Register<CalculatorViewModel>();




            SimpleIoc.Default.Register<TABViewModel>();
            SimpleIoc.Default.Register(() => navigation);

        }



        public NavigationService NavigationService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NavigationService>();
            }
        }



        #region MyRegion

        #region ROOT

        ///Gets the ROOT Property///


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public RootViewModel Root
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RootViewModel>();
            }
        }


        #endregion

        #region MENU
        ///Gets the MENU Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public MenuViewModel Menu
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MenuViewModel>();
            }
        }

        #endregion

        #region HOME
        ///Gets the Home Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        #endregion

        #region PSE

        ///Gets the PSE Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public PSEViewModel PSE
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PSEViewModel>();
            }
        }
        #endregion

        #region CRYPTO
        ///Gets the CRYPTO Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public CryptoViewModel Crypto
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CryptoViewModel>();
            }
        }
        #endregion


        #region Search
        ///Gets the CRYPTO Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }
        #endregion


        #region Fibo
        ///Gets the CRYPTO Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public FibonacciViewModel Fibonacci
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FibonacciViewModel>();
            }
        }
        #endregion

        #region Calculator
        ///Gets the CRYPTO Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public CalculatorViewModel Calculator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CalculatorViewModel>();
            }
        }
        #endregion

        #region PortfolioAddEdit

        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public PortfolioAddEditViewModel PortfolioAddEdit
        {
            get
            {
                return ServiceLocator.Current
                    .GetInstance<PortfolioAddEditViewModel>();
            }
        }
        #endregion

        #region TAB
        ///Gets the CRYPTO Property///
        ///

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
        "CA1822:MarkMembersAsStatic",
        Justification = "This non-static member is needed for data binding purposes.")]

        public TABViewModel TAB
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TABViewModel>();
            }
        }
        #endregion

        #endregion


    }
}
