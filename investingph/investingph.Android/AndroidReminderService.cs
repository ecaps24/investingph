using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using investingph.Services;
using investingph.Converters;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(investingph.Droid.AndroidReminderService))]
namespace investingph.Droid
{
    public class AndroidReminderService : MarketTimeConverter, IReminderService
    {
        int counter = 0;

        public void Remind(string title, string message, DateTime dateTime, string type)
        {
            Intent alarmIntent = new Intent(Forms.Context, typeof(AlarmReceiver));

            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(Forms.Context, ++counter, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)Forms.Context.GetSystemService(Context.AlarmService);

            var millis = Convert.ToInt64((DateTime.Now - DateTime.Today.Date).TotalMilliseconds);
            var sampleTime = ManilaTime; //ManilaDay.AddHours(16).AddMinutes(50);

            var triggerOpen = Convert.ToInt64((MarketOpen - ManilaDay.Date).TotalMilliseconds);
            var triggerClose = Convert.ToInt64((MarketClose - ManilaDay).TotalMilliseconds);

            var intervalDay = AlarmManager.IntervalDay;
            Java.Util.Calendar calendar = Java.Util.Calendar.Instance;

            if (type == "Open")
            {

                calendar.Set(
                    Java.Util.CalendarField.HourOfDay, MarketOpen.Hour);
                calendar.Set(
                    Java.Util.CalendarField.Minute, MarketOpen.Minute);
                alarmManager.SetInexactRepeating(
                    AlarmType.RtcWakeup, calendar.TimeInMillis, intervalDay, pendingIntent);
            }
            else if (type == "Close")
            {
                calendar.Set(
                    Java.Util.CalendarField.HourOfDay, MarketClose.Hour);
                calendar.Set(
                    Java.Util.CalendarField.Minute, MarketClose.Minute);
                alarmManager.SetInexactRepeating(
                    AlarmType.RtcWakeup, calendar.TimeInMillis, intervalDay, pendingIntent);
            }
        }
    }
}