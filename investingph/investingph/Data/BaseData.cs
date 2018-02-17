using investingph.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace investingph.Data
{
    public class BaseData
    {
        public string StatusMessage { get; set; }
        public SQLiteAsyncConnection conn;
        public int result = 0;


    }
}
