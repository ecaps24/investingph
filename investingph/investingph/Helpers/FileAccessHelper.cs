using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Helpers
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
}
