using investingph.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace investingph.ViewModels
{
    public class FibonacciViewModel : BaseViewModel
    {
        const double Ret0 = 0;
        const double Ret236 = 0.236;
        const double Ret382 = 0.382;
        const double Ret50 = 0.50;
        const double Ret618 = 0.618;
        const double Ret764 = 0.764;
        const double Ret100 = 1;
        const double Ret1382 = 1.382;

        const double Ext2618 = 2.618;
        const double Ext200 = 2;
        const double Ext1618 = 1.618;
        const double Ext1382 = 1.382;
        const double Ext100 = 1;
        const double Ext618 = 0.618;
        const double Ext50 = 0.5;
        const double Ext382 = 0.382;
        const double Ext236 = 0.236;


        #region Retracement Properties

        private double retrace0;
        public double Retrace0
        {
            get
            {
                return retrace0;
            }
            set
            {
                retrace0 = value;
                OnPropertyChanged("Retrace0");
            }
        }

        private double retrace236;
        public double Retrace236
        {
            get
            {
                return retrace236;
            }
            set
            {
                retrace236 = value;
                OnPropertyChanged("Retrace236");
            }
        }

        private double retrace382;
        public double Retrace382
        {
            get
            {
                return retrace382;
            }
            set
            {
                retrace382 = value;
                OnPropertyChanged("Retrace382");
            }
        }

        private double retrace50;

        public double Retrace50
        {
            get
            {
                return retrace50;
            }
            set
            {
                retrace50 = value;
                OnPropertyChanged("Retrace50");
            }
        }

        private double retrace618;

        public double Retrace618
        {
            get
            {
                return retrace618;
            }
            set
            {
                retrace618 = value;
                OnPropertyChanged("Retrace618");
            }
        }

        private double retrace764;

        public double Retrace764
        {
            get
            {
                return retrace764;
            }
            set
            {
                retrace764 = value;
                OnPropertyChanged("Retrace764");
            }
        }

        private double retrace100;
        public double Retrace100
        {
            get
            {
                return retrace100;
            }
            set
            {
                retrace100 = value;
                OnPropertyChanged("Retrace100");
            }
        }

        private double retrace1382;
        public double Retrace1382
        {
            get
            {
                return retrace1382;
            }
            set
            {
                retrace1382 = value;
                OnPropertyChanged("Retrace1382");
            }
        }

        #endregion


        #region Extension Properties



        private double extension2618;

        public double Extension2618
        {
            get { return extension2618; }
            set
            {
                extension2618 = value;
                OnPropertyChanged("Extension2618");

            }
        }



        private double extension200;

        public double Extension200
        {
            get { return extension200; }
            set
            {
                extension200 = value;
                OnPropertyChanged("Extension200");

            }
        }

        private double extension1618;

        public double Extension1618
        {
            get { return extension1618; }
            set
            {
                extension1618 = value;
                OnPropertyChanged("Extension1618");

            }
        }

        private double extension1382;

        public double Extension1382
        {
            get { return extension1382; }
            set
            {
                extension1382 = value;
                OnPropertyChanged("Extension1382");

            }
        }


        private double extension100;

        public double Extension100
        {
            get { return extension100; }
            set
            {
                extension100 = value;
                OnPropertyChanged("Extension100");

            }
        }

        private double extension618;

        public double Extension618
        {
            get { return extension618; }
            set
            {
                extension618 = value;
                OnPropertyChanged("Extension618");

            }
        }

        private double extension50;

        public double Extension50
        {
            get { return extension50; }
            set
            {
                extension50 = value;
                OnPropertyChanged("Extension50");

            }
        }

        private double extension382;

        public double Extension382
        {
            get { return extension382; }
            set
            {
                extension382 = value;
                OnPropertyChanged("Extension382");

            }
        }

        private double extension236;

        public double Extension236
        {
            get { return extension236; }
            set
            {
                extension236 = value;
                OnPropertyChanged("Extension236");

            }
        }

        #endregion

        private double highPrice;

        public double HighPrice
        {
            set
            {
                highPrice = value;

                OnPropertyChanged("HighPrice");
                Refresh(highPrice, lowPrice, custom);


            }
            get { return highPrice; }

        }

        private double lowPrice;

        public double LowPrice
        {
            set
            {
                lowPrice = value;


                OnPropertyChanged("LowPrice");
                Refresh(highPrice, lowPrice, custom);

            }
            get { return lowPrice; }
        }

        private double custom;

        public double Custom
        {

            set
            {
                custom = value;
                OnPropertyChanged("Custom");
                Refresh(highPrice, lowPrice, custom);
            }
            get { return custom; }
        }

        private ObservableCollection<Controls> controlList;

        public ObservableCollection<Controls> ControlList
        {
            get { return controlList; }
            set { controlList = value; }
        }


        public FibonacciViewModel()
        {
            Title = "Fibonacci Calculator";
            _deductValueCommand = new Command<Entry>(DeductValue);
            _addValueCommand = new Command<Entry>(AddValue);


        }

        public void Refresh(double hPrice, double lPrice, double Custom = 0)
        {
            var zeroRes = (hPrice * lPrice) == 0 ||
                lPrice > hPrice ||
                lPrice == 0 ||
                hPrice == 0;

            Retrace0 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret0);
            Retrace236 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret236);
            Retrace382 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret382);
            Retrace50 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret50);
            Retrace618 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret618);
            Retrace764 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret764);
            Retrace100 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret100);
            Retrace1382 = zeroRes ? 0 : ComputeRetracement(hPrice, lPrice, Ret1382);


            Extension2618 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext2618, Custom);
            Extension200 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext200, Custom);
            Extension1618 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext1618, Custom);
            Extension1382 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext1382, Custom);
            Extension100 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext100, Custom);
            Extension618 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext618, Custom);
            Extension50 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext50, Custom);
            Extension382 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext382, Custom);
            Extension236 = zeroRes ? 0 : ComputeExtension(hPrice, lPrice, Ext236, Custom);



        }

        public double ComputeRetracement(double HighPrice,
            double LowPrice,
            double RetracementLevel)
        {
            //high price minus low price
            //high price minus (high price minus low price) % N
            double result = HighPrice - (HighPrice - LowPrice) * RetracementLevel;
            return result;

        }

        public double ComputeExtension(double HighPrice,
            double LowPrice,
            double RetracementLevel,
            double Custom = 0)
        {
            //highprice + (highprice - lowprice) * N
            double nPrice = Custom == 0 ? HighPrice : Custom;
            double result = nPrice + (nPrice - LowPrice) * RetracementLevel;
            return result;
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

        public void DeductValue(Entry entry)
        {
            try
            {

                double price = Convert.ToDouble(entry.Text);
                price = price != 0 ? price -0.01 : price;
                string name = entry.Placeholder;
                if (name.Contains("High"))
                {
                    HighPrice = price;
                }
                else if (name.Contains("Low"))
                {
                    LowPrice = price;
                }
                else if (name.Contains("Pullback"))
                {
                    Custom = price;
                }
            }
            catch (Exception)
            {

                return;
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

        public void AddValue(Entry entry)
        {
            try
            {

                double price = Convert.ToDouble(entry.Text);
                price = price != 0 ? price + 0.01 : price;
                string name = entry.Placeholder;
                if (name.Contains("High"))
                {
                    HighPrice = price;
                }
                else if (name.Contains("Low"))
                {
                    LowPrice = price;
                }
                else if (name.Contains("Pullback"))
                {
                    Custom = price;
                }
            }
            catch (Exception)
            {

                return;
            }
        }


    }

}
