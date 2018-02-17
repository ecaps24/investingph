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
    public class CompanyData : BaseData
    {
        public CompanyData()
        {
        }

        public async Task<List<Company>> GetCompanyList()
        {
            List<Company> companyList = new List<Company>();
            var assembly = typeof(CompanyData)
            .GetTypeInfo().Assembly;
            Stream stream = assembly
                    .GetManifestResourceStream("investingph.Files.Company.txt");
            
            using (var reader = new StreamReader(stream))
            {
                // reader.readline
                var text  = await reader.ReadToEndAsync();
                List<Company> company = JsonConvert
                    .DeserializeObject<List<Company>>(text);
                //List<Company> company = new List<Company>(result);
                companyList = new List<Company>(company);
                //while (!reader.EndOfStream)
                //{
                //    var line = reader.ReadLineAsync();
                //    var values = line.Split('|');
                //    companyList.Add(new Company
                //    {
                //        Symbol = values[0],
                //        CompanyName = values[1]
                //    });

                //}
            }
   
           

            return companyList;

        }

        public async Task<List<string>> GetCompanies()
        {
            var assembly = typeof(CompanyData)
                .GetTypeInfo().Assembly;
            Stream stream = assembly
                    .GetManifestResourceStream("investingph.Files.Company.txt");
            List<string> companies = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    companies.Add(line);
                }
            }
            await Task.Delay(10);
            return companies;
        }

    }
}


//public CompanyData(string companyCSV)
//{
//    try
//    {
//        conn = new SQLiteAsyncConnection(dbPath);
//        conn.CreateTableAsync<Company>();
//    }
//    catch (Exception e)
//    {

//        StatusMessage = e.Message.ToString();
//    }
//}


//public async Task AddCompanies(IEnumerable<Company> companies)
//{
//    result = 0;
//    try
//    {
//        result = await conn.InsertAllAsync(companies);
//    }
//    catch (Exception e)
//    {

//        StatusMessage = $"Failed to add companies, error: {e.Message}";

//    }
//}

//public async Task<List<Company>> GetCompaniesAsync()
//{
//    List<Company> resultList = new List<Company>();
//    try
//    {
//        resultList = await conn.Table<Company>().ToListAsync();
//    }
//    catch (Exception e)
//    {
//        StatusMessage = $"Error: {e.Message}";
//    }

//    return resultList;
//}