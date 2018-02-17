using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using SQLite;

namespace investingph.Services
{
    public class SQLiteService : ISQLite
    {
        //public SQLiteConnection GetConnection()
        //{
        //    var sqliteFileName = "investingph.db3";
        //    string docPath = Environment.GetFolderPath(Environment
        //        .SpecialFolder.Personal);
        //    var path = Path.Combine(docPath, sqliteFileName);
        //    Debug.WriteLine(path);
        //    if (!File.Exists(path)) File.Create(path);
        //    var plat = new SQLite.pla

        //}
        public SQLiteConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}
