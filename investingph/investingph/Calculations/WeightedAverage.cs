using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Calculations
{
    public class WeightedAverage
    {


        public double Weight(double shares, double totalShares)
        {
            var a = shares / totalShares;
            return a;
        }

        public double WeightedPrice(double shares, double price, double totalShares)
        {
            var a = price * Weight(shares, totalShares);
            return a;
        }

        public double AverageWeightedPrice(double shares1, double price1,
                double shares2, double price2)
        {
            double awp1 = WeightedPrice(shares1, price1, TotalShares(shares1, shares2));
            double awp2 = WeightedPrice(shares2, price2, TotalShares(shares1, shares2));
            return awp1 + awp2;
        }

        public double TotalShares(double shares1, double shares2)
        {
            return shares1 + shares2;
        }



    }
}
