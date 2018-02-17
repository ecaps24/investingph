using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Calculations
{
    public class WeightedAverage
    {


        public double Weight(int shares, int totalShares)
        {
            var a = Convert.ToDouble(shares) / Convert.ToDouble(totalShares);
            return a;
        }

        public double WeightedPrice(int shares, double price, int totalShares)
        {
            var a = price * Weight(shares, totalShares);
            return a;
        }

        public double AverageWeightedPrice(int shares1, double price1, int shares2, double price2)
        {
            double awp1 = WeightedPrice(shares1, price1, TotalShares(shares1, shares2));
            double awp2 = WeightedPrice(shares2, price2, TotalShares(shares1, shares2));
            return awp1 + awp2;
        }

        public int TotalShares(int shares1, int shares2)
        {
            return shares1 + shares2;
        }



    }
}
