using System;
using investingph.Services;
using System.Collections.Generic;
using System.Text;
using investingph.Models;
using investingph.Data;
using System.Threading.Tasks;

namespace investingph.Converters
{
    public class MarketTimeConverter
    {
        public DateTime ManilaTime { get { return DateTime.UtcNow.AddHours(8); } }
        public DateTime ManilaDay { get { return ManilaTime.Date; } }
        public DateTime SystemTime { get { return DateTime.Now; } }
        public DateTime MarketOpen
        {
            get
            {
                return ManilaDay.
                 AddHours(9).AddMinutes(30);
            }
        }

        public DateTime MarketClose
        {
            get
            {
                return ManilaDay.
                 AddHours(15).AddMinutes(30);
            }
        }
        public DateTime MarketRecess { get { return ManilaDay.AddHours(12); } }
        public DateTime MarketReOpen { get { return ManilaDay.AddHours(13.5); } }
        public DateTime SampleTime
        {
            get
            {
                return ManilaDay.
                    AddHours(ManilaTime.Hour).
                    AddMinutes(20);
            }
        }

        public DateTime MarketPreOpen { get { return ManilaDay.AddHours(9); } }

        public Boolean Weekend
        {
            get
            {
                var day = ManilaTime.DayOfWeek;
                if (day == DayOfWeek.Sunday ||
                    day == DayOfWeek.Saturday)
                    return true;
                else return false;

            }
        }

        public async Task<bool> IsBusinessDay()
        {
            //  Holiday holidays = new Holiday();
            // var exists = holidays(h => h.Date == ManilaDay);
            HolidayData hData = new HolidayData();
            var exists = await hData.IsHoliday(ManilaDay);
            if (ManilaTime.DayOfWeek != DayOfWeek.Saturday && ManilaTime.DayOfWeek != DayOfWeek.Sunday && !exists)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public async Task<bool> MarketActive()
        {
                var open = ManilaTime >= MarketOpen && ManilaTime < MarketClose.AddMinutes(10);
                 
                if (await IsBusinessDay() && open)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        

    }
}
