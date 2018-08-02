using investingph.Calculations;
using investingph.Converters;
using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace investingph.ViewModels
{
    public class WeightedAverageViewModel : BaseViewModel
    {

        private double currentShares;

        public double CurrentShares
        {
            get { return currentShares; }
            set
            {
                currentShares = value;
                OnPropertyChanged("CurrentShares");
                Calculate();
            }
        }

        private double averagePrice;

        public double AveragePrice
        {
            get { return averagePrice; }
            set
            {
                averagePrice = value;
                OnPropertyChanged("AveragePrice");
                Calculate();
            }
        }



        private double newSharesPrice;

        public double NewSharesPrice
        {
            get { return newSharesPrice; }
            set
            {
                newSharesPrice = value;
                OnPropertyChanged("NewSharesPrice");
                Calculate();
            }
        }

        private double totalShares;

        public double TotalShares
        {
            get { return totalShares; }
            set
            {
                totalShares = value;
                OnPropertyChanged("TotalShares");

            }
        }

        private double totalAveragePrice;

        public double TotalAveragePrice
        {
            get { return totalAveragePrice; }
            set
            {
                totalAveragePrice = value;
                OnPropertyChanged("TotalAveragePrice");

            }
        }

        private double currentPortfolioValue;

        public double CurrentPortfolioValue
        {
            get { return currentPortfolioValue; }
            set
            {
                currentPortfolioValue = value;
                OnPropertyChanged("CurrentPortfolioValue");

            }
        }


        private double newPortfolioValue;

        public double NewPortfolioValue
        {
            get { return newPortfolioValue; }
            set
            {
                newPortfolioValue = value;
                OnPropertyChanged("NewPortfolioValue");

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

        private double buyingPower;

        public double BuyingPower
        {
            get { return buyingPower; }
            set
            {
                buyingPower = value;
                OnPropertyChanged("BuyingPower");

            }
        }

        private double newShares;

        public double NewShares
        {
            get { return newShares; }
            set
            {
                newShares = value;
                OnPropertyChanged("NewShares");
                Calculate();
            }
        }



        public WeightedAverageViewModel()
        {
            Title = "Average";
            //FibonacciViewModel fvm = new FibonacciViewModel();
            _deductValueCommand = new Command<Entry>(DeductValue);
            _addValueCommand = new Command<Entry>(AddValue);
        }

        public void Calculate()
        {
            WeightedAverage weighted = new WeightedAverage();
            var zero = CurrentShares == 0;
            //NewShares =Math.Round(BuyingPower / NewSharesPrice);
            BuyingPower = zero ? 0 : Math.Round(NewShares * NewSharesPrice);
            TotalShares = zero ? 0 : Math.Round(weighted.TotalShares(CurrentShares, NewShares));
            TotalAveragePrice = zero ? 0 : Math.Round(weighted.AverageWeightedPrice(
                CurrentShares, AveragePrice, NewShares, NewSharesPrice), 2);
            Capital = zero ? 0 : Math.Round(TotalShares * TotalAveragePrice, 2);


        }
        private Command<Entry> _deductValueCommand;
        public Command<Entry> DeductValueCommand
        {
            get
            {
                return _deductValueCommand;
            }
            set
            {
                _deductValueCommand = value;
            }
        }

        private Command<Entry> _addValueCommand;
        public Command<Entry> AddValueCommand
        {
            get
            {
                return _addValueCommand;
            }
            set
            {
                _addValueCommand = value;
            }
        }

        public void DeductValue(Entry entry)
        {
            double val = 0;
            try
            {
                val = Convert.ToDouble(entry.Text);
                string name = entry.Placeholder;
                AssignValues(name, val, "Deduct");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void AddValue(Entry entry)
        {
            double val = 0;
            try
            {
                val = Convert.ToDouble(entry.Text);
                string name = entry.Placeholder;
                AssignValues(name, val, "Add");
                //val = val != 0 ? val + .01 : val;

            }
            catch (Exception e)
            {

                return;
            }

        }

        public void AssignValues(string controlName, double value, string action)
        {
            try
            {


                IncrementConverter converter = new IncrementConverter();
                if (controlName.Contains("Current"))
                {
                    CurrentShares = action == "Add" ? value + 100 : value - 100;

                }
                else if (controlName.Contains("Average"))
                {
                    AveragePrice = action == "Add" ? value + ConvertValue(value) :
                        value - ConvertValue(value);
                }
                else if (controlName.Contains("New Shares"))
                {
                    NewShares = action == "Add" ? value + 100 : value - 100; ;
                }
                else if (controlName.Contains("New"))
                {
                    NewSharesPrice = action == "Add" ? value + ConvertValue(value) :
                        value - ConvertValue(value);
                }

            }
            catch (Exception)
            {

                return;
            }
        }

        public double ConvertValue(double val)
        {
            return val >= 1000 ? 1
                : val >= 100 ? 0.1

                : .01;
        }


    }
}
