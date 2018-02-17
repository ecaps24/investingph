using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Models
{
    [Table("watchList")]
    public class WatchList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DateAdded => DateTime.Now;
        [Unique, MaxLength(10), NotNull]
        public string Symbol { get; set; }

    }
}
