using System;
using System.Collections.Generic;
using System.Text;

namespace investingph.Interfaces
{
    public interface IFiles
    {
        string ReadTextFile(string path, string fileName);
        void WriteTextFile(string path, string filename, string stringToWrite);
        string RootDirectory();
        DateTime LastWriteDate(string path, string filename);
        Boolean FileExists(string path, string filename);
    }
}
