using investingph.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace investingph.Data
{
    public class PortfolioData : BaseData
    {
        public PortfolioData(string dbPath)
        {
            try
            {
                conn = new SQLite.SQLiteAsyncConnection(dbPath);
             //   conn.DropTableAsync<Portfolio>().Wait();
                conn.CreateTableAsync<Portfolio>().Wait();
            }
            catch (Exception e)
            {

                StatusMessage = string.Format(e.Message);

            }
        }

        public async Task AddNewPortfolioAsync(Portfolio port)
        {
            int result = 0;
            try
            {
                result = await conn.InsertAsync(port);
                StatusMessage = $"{port.Symbol} has been added to Portfolio.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Failed to add {port.Symbol}. " +
                    $"Error: {e.Message}";
            }
        }


        public async Task UpdatePortfolioAsync(Portfolio port)
        {
            int result = 0;
            try
            {
                result=await conn.UpdateAsync(port);
                StatusMessage = $"{port.Symbol} has been updated.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Failed to update {port.Symbol}. " +
                    $"Error: {e.Message}";
            }
        }


        public async Task DeletePortfolioItemAsync(Portfolio port)
        {

            try
            {
                result = await conn.DeleteAsync(port);
                StatusMessage = $"{port.Symbol} has been removed from Portfolio.";
            }
            catch (Exception e)
            {
                StatusMessage = $"Failed to delete {port.Symbol}." +
                    $" Error: {e.Message}";
            }
        }

        public async Task<List<Portfolio>> GetPortfolioListAsync()
        {
            List<Portfolio> list = new List<Portfolio>();
            try
            {
                list = await conn.Table<Portfolio>().ToListAsync();
            }
            catch (Exception e)
            {

                StatusMessage = $"Error: {e.Message}";
            }
            return list;
            
        }

        public async Task<Portfolio> _Portfolio(string symbol)
        {
            Portfolio p = new Portfolio();
            try
            {
                p = await conn.Table<Portfolio>().Where(
                    e => e.Symbol == symbol).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {

                throw;
            }
            return p;
        }

        public async Task<Boolean> PortfolioExists(string symbol)
        {
            List<Portfolio> list = await GetPortfolioListAsync();
            return list.Exists(e => e.Symbol == symbol);
        }
        
    }
}
