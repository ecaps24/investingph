using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using investingph.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace investingph.Data
{
    public class WatchListData : BaseData
    {
        public bool Exists { get; set; }
        public WatchListData(string dbPath)
        {
            try
            {
                conn = new SQLiteAsyncConnection(dbPath);
                //   conn.DropTableAsync<WatchList>().Wait();
                conn.CreateTableAsync<WatchList>().Wait();
            }
            catch (Exception e)
            {
                StatusMessage = string.Format(e.Message);
            }
        }

       public async Task AddNewStockAsync(WatchList watch)
        {
     
            try
            {
                result = await conn.InsertAsync(watch);
                StatusMessage = $"{watch.Symbol} has been added to WatchList";

            }
            catch (Exception e)
            {

                StatusMessage = $"Failed to add {watch.Symbol}. Stock already exists in WatchList";

            }
            Debug.WriteLine(StatusMessage);

        }

        public async Task<ObservableCollection<WatchList>> GetWatchListAsync()
        {
            ObservableCollection<WatchList> ow = new ObservableCollection<WatchList>();
            try
            {
                var l= await conn.Table<WatchList>().ToListAsync();
                ow = new ObservableCollection<WatchList>(l);
            }
            catch (Exception e)
            {
                StatusMessage = $"Error getting watchlist. {e.Message}";
                Debug.WriteLine(StatusMessage);

                List<WatchList> wl = new List<WatchList>
                {
                    new WatchList { Symbol = e.Message }
                };
                var l= wl.ToList();
                ow = new ObservableCollection<WatchList>(l);

            }

            return ow;
        }

        public async Task RecordExists()
        {
            var allItems = await conn.Table<WatchList>().ToListAsync();
            int count = allItems.Count();
            Exists= count == 0 ? false : true;
        }

        public async Task DeleteStockAsync(string symbol)
        {
            var items = await conn.Table<WatchList>().ToListAsync();
            var count = items.Count();
            if (count==1)
            {
                StatusMessage = "Watchlist must have at least one record.";
                return;
            }

            ObservableCollection<WatchList> list = new ObservableCollection<WatchList>(
                     await GetWatchListAsync());
            var watch = list.Where(s => s.Symbol == symbol).FirstOrDefault();
            try
            {
                result = await conn.DeleteAsync(watch);
                StatusMessage = $"{symbol} has been removed from WatchList.";

            }
            catch (Exception e)
            {
                StatusMessage = $"Failed to delete {symbol}. Error: {e.Message}";

            }

            Debug.WriteLine(StatusMessage);
        }

    }
}
