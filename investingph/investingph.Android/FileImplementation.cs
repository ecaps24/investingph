
using Java.IO;
using investingph.Droid;
using investingph.Interfaces;
using System;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(FilesImplementation))]
namespace investingph.Droid
{
    public class FilesImplementation : IFiles
    {
        public FilesImplementation()
        {
        }


        public string ReadTextFile(string path, string fileName)
        {
            //throw new NotImplementedException();
            using (System.IO.StreamReader sr = new System.IO.StreamReader(System.IO.Path.Combine(path, fileName)))
            {
                string line = sr.ReadToEnd();
                sr.Close();
                return line;
            }
        }

        private string creaFileName(string directory, string fileName)
        {
            string path = RootDirectory();
            string file = System.IO.Path.Combine(path, fileName);
            return file;
        }

        public void WriteTextFile(string path, string fileName, string stringToWrite)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(path, fileName), false))
            {
                sw.WriteLine(stringToWrite);
                sw.Close();
            }
        }

        public string RootDirectory()
        {
            File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            return path.AbsolutePath;
        }



        public DateTime LastWriteDate(string path, string filename)
        {
            DateTime dt = DateTime.UtcNow.AddHours(8);
            try
            {
                dt = System.IO.File.GetLastWriteTimeUtc
                    (System.IO.Path.Combine(path, filename)).AddHours(8);

            }
            catch (Exception e)
            {

                throw;
            }
            return dt;
        }

        public bool FileExists(string path, string filename)
        {
            return System.IO.File.Exists(System.IO.Path.Combine(path, filename));
        }
    }
}