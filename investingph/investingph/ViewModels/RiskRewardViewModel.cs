using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace investingph.ViewModels
{
    public class RiskRewardViewModel : BaseViewModel
    {
        public RiskRewardViewModel()
        {
            Title = "Risk Reward";
        }

        private double shares;

        public double Shares
        {
            get { return shares; }
            set
            {
                shares = value;
                OnPropertyChanged("Shares");

                Calculate();


            }
        }

        private double capital;

        public double Capital
        {
            get { return capital; }
            set
            {
                capital = value;
                OnPropertyChanged("Capital");

            }
        }


        private double targetPrice;

        public double TargetPrice
        {
            get { return targetPrice; }
            set
            {
                targetPrice = value;
                OnPropertyChanged("TargetPrice");
                Calculate();
            }
        }

        private double entryPrice;

        public double EntryPrice
        {
            get { return entryPrice; }
            set
            {
                entryPrice = value;
                OnPropertyChanged("EntryPrice");

                Calculate();

            }
        }

        private double stopLoss;

        public double StopLoss
        {
            get { return stopLoss; }
            set
            {
                stopLoss = value;
                OnPropertyChanged("StopLoss");
                Calculate();
            }
        }

        private double percentGain;

        public double PercentGain
        {
            get { return percentGain; }
            set
            {
                percentGain = value;
                OnPropertyChanged("PercentGain");
            }
        }

        private double percentLoss;

        public double PercentLoss
        {
            get { return percentLoss; }
            set
            {
                percentLoss = value;
                OnPropertyChanged("PercentLoss");
            }
        }

        private double amountGain;

        public double AmountGain
        {
            get { return amountGain; }
            set
            {
                amountGain = value;
                OnPropertyChanged("AmountGain");
            }
        }


        private double amountLoss;

        public double AmountLoss
        {
            get { return amountLoss; }
            set
            {
                amountLoss = value;
                OnPropertyChanged("AmountLoss");
            }
        }





        public void Calculate()
        {
            var zero = Shares == 0;
            try
            {
                //  Shares = zero ? 0 : Math.Round(ComputeShares());
                Capital = zero ? 0 : Math.Round(ComputeCapital(), 2);
                PercentGain = zero ? 0 : Math.Round(ComputePercentGain() * 100, 2);
                PercentLoss = zero ? 0 : Math.Round(ComputePercentLoss() * 100, 2);
                AmountGain = zero ? 0 : ComputeAmountGain();
                AmountLoss = zero ? 0 : ComputeAmountLoss();
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


        }

        public double ComputeShares()
        {
            return Capital / EntryPrice;
        }

        public double ComputeCapital()
        {
            double res = 0;
            try
            {
                res = Shares * EntryPrice;
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return res;
        }

        public double ComputePercentGain()
        {
            return (TargetPrice - EntryPrice) / EntryPrice;
        }

        public double ComputePercentLoss()
        {
            return (StopLoss - EntryPrice) / EntryPrice;
        }

        public double ComputeAmountGain()
        {
            return Capital * ComputePercentGain();
        }

        public double ComputeAmountLoss()
        {
            return Capital * ComputePercentLoss();
        }

    }
}
