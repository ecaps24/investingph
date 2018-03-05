using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using investingph.Converters;
using investingph.Services;

namespace investingph.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new string[]
    { "android.intent.action.BOOT_COMPLETED" },
        Priority = (int)IntentFilterPriority.LowPriority)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public bool MarketActive { get; set; }
        public override void OnReceive(Context context, Intent intent)
        {
            int counter = 0;
            MarketTimeConverter marketTime = new MarketTimeConverter();
            ConnectionStatus conn = new ConnectionStatus();
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notIntent = new Intent(context, 
                typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(
                context, ++counter, notIntent, 
                PendingIntentFlags.UpdateCurrent);

            var manager = NotificationManagerCompat.From(
                context);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

            //generate notications
            var builder = new NotificationCompat.Builder(context)
                                .SetContentIntent(contentIntent)
                                .SetSmallIcon(Resource.Drawable.stocks)

                                .SetContentTitle(title)
                                .SetContentText(message)
                                .SetStyle(style)
                                .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                                .SetAutoCancel(true)

                                .SetDefaults(1);

            var notification = builder.Build();

            CheckMarketActive();

            if (!MarketActive) return;

            var open = title.Contains("Open");
            var close = title.Contains("Close");
            var timeAhead = 10;
            var ahead = marketTime.SampleTime.AddMinutes(30);
            var t = marketTime.SampleTime > marketTime.ManilaTime;

            var withinTimeFrame = (marketTime.ManilaTime.Ticks
                                    > marketTime.MarketOpen.Ticks
                                    && marketTime.ManilaTime.Ticks <
                                    marketTime.MarketOpen.AddMinutes(timeAhead).Ticks);

            var withinTimeFrameClose = (marketTime.ManilaTime.Ticks
                                    > marketTime.MarketClose.Ticks
                                    && marketTime.ManilaTime.Ticks <
                                    marketTime.MarketClose.AddMinutes(timeAhead).Ticks);

            if (open && !withinTimeFrame) return;
            if (close && !withinTimeFrameClose) return;

            manager.Notify(++counter, notification);

        }

        public async void CheckMarketActive()
        {
            MarketTimeConverter market = new MarketTimeConverter();
            MarketActive = await market.MarketActive();
        }
    }
}