using investingph.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace investingph.Data
{
    public class HolidayData : BaseData
    {
        public HolidayData()
        {

        }

        public async Task<List<Holiday>> GetHolidays()
        {
            List<Holiday> holidayList = new List<Holiday>();
            var assembly = typeof(HolidayData)
            .GetTypeInfo().Assembly;
            Stream stream = assembly
                    .GetManifestResourceStream("investingph.Files.Holidays.txt");

            using (var reader = new StreamReader(stream))
            {
                var text = await reader.ReadToEndAsync();
                List<Holiday> holiday = JsonConvert
                    .DeserializeObject<List<Holiday>>(text);
                holidayList = new List<Holiday>(holiday);
            }
            return holidayList;
        }

        public async Task<bool> IsHoliday(DateTime date)
        {
            List<Holiday> holidays = new List<Holiday>();
            holidays = await GetHolidays();
            return  holidays.Exists(d => d.HolidayDate == date);

        }
    }
}
