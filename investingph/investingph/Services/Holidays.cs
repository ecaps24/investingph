using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Services
{
    public class Holidays
    {
        private List<DateTime> dates;

        public List<DateTime> Dates
        {
            get
            {
                string year = DateTime.Now.Year.ToString();
                List<DateTime> holidays = new List<DateTime>
                {

                        DateTime.Parse("01/01/" + year),
                        DateTime.Parse("02/16/" + year),
                        DateTime.Parse("02/25/" + year),
                        DateTime.Parse("03/29/" + year),
                        DateTime.Parse("03/30/" + year),
                        DateTime.Parse("03/31/" + year),
                        DateTime.Parse("04/09/" + year),
                        DateTime.Parse("05/01/" + year),
                        DateTime.Parse("06/12/" + year),
                        DateTime.Parse("06/15/" + year),
                        DateTime.Parse("08/21/" + year),
                        DateTime.Parse("08/21/" + year),
                        DateTime.Parse("08/27/" + year),
                        DateTime.Parse("11/01/" + year),
                        DateTime.Parse("11/02/" + year),
                        DateTime.Parse("11/30/" + year),
                        DateTime.Parse("12/24/" + year),
                        DateTime.Parse("12/25/" + year),
                        DateTime.Parse("12/30/" + year),
                        DateTime.Parse("12/31/" + year)



                };
                dates = holidays;
                return dates;
            }
            set { dates = value; }
        }








    }

}
