using investingph.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Info
{
    public class Instructions : ServerStatus
    {
        private List<string> tips;

        public List<string> Tips
        {
            get
            {

                if (Busy)
                {
                    return BusyList();
                }


                List<string> list = new List<string>
                    {
                         "LONG PRESS to an item to show more options",
                         "You can switch between TABS by swiping left/ right",
                         "Pull the list down to Refresh",
                         "PORTFOLIO is a list of stocks you have presumably bought." ,
                         "In order to buy stocks, you can either use an online broker, or seek the assistance of a stockbroker who is licensed to purchase securities on your behalf. ",
                         "Trading on the PSE pre-opens at 9:00 A.M.; opens at 9:30 A.M.; is in recess between 12:00 and 1:30 P.M.; pre-closes at 3:15 P.M.; in run-off from 3:20 P.M.; and closes at 3:30 P.M.",
                         "You can practice investing/trading using this platform, simply Long Press an item and click BUY STOCK or add to PORTFOLIO",

                            "To remove a stock from the portfolio, long press and tap DELETE",
                        "To Edit a portfolio, long press and tap EDIT",
                            "Search a stock using Search Bar to add stocks to the list",
                            "TAP the SORT button to show Sort Options. Data can be sorted " +
                        "according to Gainers, Active and Volume",
                        "TAP the MENU Button at the top left corner to show other stuff"

                    };
                tips = list;
                return tips;
            }
            set
            {

                tips = value;
            }
        }

        private List<string> BusyList()
        {
            List<string> list = new List<string>
                {"Market is on pre-open state. Come back at 9:30 AM." };
            return list;

        }

        private List<string> stockInstructions;

        public List<string> StockInstructions
        {
            get
            {
                List<string> list = new List<string>
                {
                    "Pull the list down to Refresh",
                    "Turn on the Auto Update switch to automatically refresh the list.",

                };
                stockInstructions = list;
                return stockInstructions;
            }
            set { stockInstructions = value; }
        }

        private List<string> watchInstructions;

        public List<string> WatchInstructions
        {
            get
            {
                List<string> list = new List<string>
                {
                    "Turn on the Auto Update to automatically refresh the list.",
                    "Search a stock using Search Bar to add stocks to the list",
                    "Long Press and item and tap Buy Stock to add a stock to Portfolio",
                    "Long Press and item and tap Delete to remove a stock to WatchList",



                };
                watchInstructions = list;
                return watchInstructions;
            }
            set { watchInstructions = value; }
        }

        private List<string> portfolioInstructions;

        public List<string> PortfolioInstructions
        {
            get
            {
                List<string> list = new List<string>
                {
                    "TAP the + Button at the bottom right to ADD Stock to Portfolio",

                    "To remove a stock from the portfolio, long press and tap DELETE",
                    "To Edit a portfolio, long press and tap EDIT"
                };
                portfolioInstructions = list;
                return portfolioInstructions;
            }
            set { portfolioInstructions = value; }
        }



        public string GetTips()
        {
            string tip = "";
            try
            {
                Random random = new Random();
                //Instructions ins = new Instructions();
                List<string> tips = new List<string>();
                tips.AddRange(Tips);
                tips.AddRange(StockInstructions);
                int rNumber = random.Next(0, tips.Count - 1);
                tip = tips[rNumber];

            }
            catch (Exception)
            {

                throw;
            }

            return tip.ToString();
        }


    }

}
