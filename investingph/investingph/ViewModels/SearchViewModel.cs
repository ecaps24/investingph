using investingph.Data;
using investingph.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace investingph.ViewModels
{
    public class SearchViewModel : StockListViewModel
    {
        public ObservableCollection<string> Companies { get; set; }

        private ObservableCollection<Company> _companyList;

        List<Company> list = new List<Company>();
        public ObservableCollection<Company> CompanyList
        {
            get { return _companyList; }
            set { _companyList = value;
                OnPropertyChanged("CompanyList");
            }
        }

        public SearchViewModel()
        {
            GetCompanyList();
        }

        public async Task GetCompanyList()
        {
            CompanyData cData = new CompanyData();
            try
            {
                list = await cData.GetCompanyList();
                CompanyList = new ObservableCollection<Company>
                    (await cData.GetCompanyList());

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            
        }

    }


}
