using investingph.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;

namespace investingph.Data
{
    public class StockData
    {
        public async Task WriteJsonToFile(string contents)
        {
            try
            {
                var assembly = typeof(StockData)
                    .GetTypeInfo().Assembly;
                Stream stream = assembly
                    .GetManifestResourceStream("investingph.Files.Company.txt");
                

                using (var file = new StreamWriter(stream))
                {
                    await file.WriteLineAsync(contents);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }


    }
}
