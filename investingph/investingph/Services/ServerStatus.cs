using investingph.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Services
{
    public class ServerStatus
    {
        public Boolean Busy { get; set; }

        public bool GetBusy()
        {
            MarketTimeConverter date = new MarketTimeConverter();
            var manila = date.ManilaTime;

            var withinTimeFrame = (manila.Ticks > date.MarketPreOpen.Ticks &&
                                    manila.Ticks < date.MarketOpen.Ticks);
            return withinTimeFrame;
        }
    }
}
