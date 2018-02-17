

using investingph.Data;
using investingph.Services;
using investingph.Views;
using Xamarin.Forms;

namespace investingph
{
	public partial class App : Application
	{

       // public static NavigationPage NavigationPage { get; private set; }
        private static RootPage RootPage;
        public static bool MenuIsPresented
        {
            get { return RootPage.IsPresented; }
            set { RootPage.IsPresented = value; }
        }

        private static Locator _locator;
        public static  Locator Locator
        {
            get { return _locator ?? (_locator=new Locator()); }
      
        }

        public static WatchListData _WatchListData { get; private set; }
        public static CompanyData _CompanyData { get; private set; }
        public static PortfolioData _PortfolioData { get;private set; }


        public App (string dbPath)
		{
			InitializeComponent();

            _WatchListData = new WatchListData(dbPath);
            _PortfolioData = new PortfolioData(dbPath);
            
            var menuPage = new MenuPage();
            var navigationPage = new NavigationPage(new HomePage());
            Locator.NavigationService.Initialize(navigationPage);
            RootPage = new RootPage
            {
                Master=menuPage,
                Detail=navigationPage
                
            };
            MainPage = RootPage;

		}

		protected override void OnStart ()
		{
            InitializeService initialize = new InitializeService();
            initialize.Initialize();
		}

		protected override void OnSleep ()
		{
            InitializeService initialize = new InitializeService();
            initialize.Initialize();
        }

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        
	}
}
