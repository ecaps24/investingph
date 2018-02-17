using investingph.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Services
{
    public class ConnectionStatus : MarketTimeConverter
    {
        private bool marketOpen;

        public bool MarketOpen
        {
            get
            {
                return CheckTime() == "Open" ? true : false;

            }
            set { marketOpen = value; }
        }

        private string CheckTime()
        {
            var val = "Close";
            var manila = 800;
            var openTime = 930 - manila;
            var closeTime = 1530 - manila;
            var recess = 1200 - manila;
            var reopen = 1330 - manila;


            var myTime = DateTime.UtcNow;
            var myTz = TimeZoneInfo.Utc;
            var a = TimeZoneInfo.ConvertTime(myTime, myTz);

            Holidays holidays = new Holidays();
            DateTime date = DateTime.UtcNow.AddHours(8);
            var exists = holidays.Dates.Exists(h => h.Date == date);



            var hourMinute = Convert.ToDouble(myTime.ToString("HHmm"));
            var myDay = myTime.DayOfWeek;


            if (!Weekend && !exists)
            {
                if ((hourMinute >= openTime && hourMinute < recess) // morning session
                    ||
                     (hourMinute >= reopen && hourMinute < closeTime) //afternoon session

                    )
                {
                    val = "Open";

                }
                else if (hourMinute >= recess && hourMinute < reopen)
                {
                    val = "Recess";
                }
                else
                {
                    val = "Close";
                }
            }

            return val;
        }



    }
}
