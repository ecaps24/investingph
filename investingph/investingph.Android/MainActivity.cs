using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using investingph.Helpers;
using Xamarin.Forms;
using investingph.Services;



namespace investingph.Droid
{
    [Activity(Label = "investingph", Icon = "@drawable/stocks", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            string dbPath = FileAccessHelper.GetLocalFilePath("investingph.db3");
            //string companyCSV = FileAccessHelper.GetLocalFilePath("Company.csv");
            LoadApplication(new App(dbPath));
            SendNotification();
            SendNotification("Close");


        }
        


        private void SendNotification()
        {
            var reminderService = DependencyService.Get<IReminderService>();
            reminderService.Remind("Stock Market Is Open",
                                "Tap to see how your stocks will perform. " 
                                , DateTime.Now, "Open");
        }


        private void SendNotification(string type = "Close")
        {
            var reminderService2 = DependencyService.Get<IReminderService>();
            reminderService2.Remind("PSE Trading Session has ended",
                                "Check how your favorite stocks perfomed by tapping this notification."
                                , DateTime.Now, "Close");

        }
    }



}

