using investingph.Interfaces;
using PCLStorage;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace investingph.Services
{
    public class LocalStorageService
    {
        public string LocalStorageName { get; set; }
        public string LocalStoragePath { get; set; }
        public string RoamingStorageName { get; set; }
        public string RoamingStoragePath { get; set; }
        public string MyStorageName { get; set; }
        public string MyStoragePath { get; set; }
        public string StringToWrite { get; set; }
        public string StringRead { get; set; }
        public string RootDirectory { get; set; }
        public string DirectoryName { get; set; }
        public string Filename { get; set; }
        public string StringToWriteNumeric { get; set; }


        public LocalStorageService()
        {
            IFolder folderLocalStorage = FileSystem.Current.LocalStorage;

            LocalStorageName = folderLocalStorage.Name;
            LocalStoragePath = folderLocalStorage.Path;


            IFolder folderRoamingStorage = FileSystem.Current.RoamingStorage;
            if (folderRoamingStorage != null)
            {
                RoamingStorageName = folderRoamingStorage.Name;
                RoamingStoragePath = folderRoamingStorage.Path;

            }

            this.WriteFileCommand = new Command(async () =>
            {
                try
                {
                    DependencyService.Get<IFiles>().WriteTextFile(DirectoryName
                        , Filename, $"{StringToWrite} - {StringToWriteNumeric}");
                }
                catch (Exception e)
                {

                    await Application.Current.MainPage
                    .DisplayAlert("Error", e.Message, "Ok");
                }
            });

            this.ReadFileCommand = new Command(async () =>
            {
                try
                {
                    StringRead = DependencyService.Get<IFiles>()
                    .ReadTextFile(DirectoryName, Filename);
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage
                    .DisplayAlert("Error", ex.Message, "Ok");
                }
            });

            RootDirectory = DependencyService.Get<IFiles>().RootDirectory();
            DirectoryName = DependencyService.Get<IFiles>().RootDirectory();


        }

        public async Task<string> ReadText()
        {
            IFolder folderLocalStorage = FileSystem.Current.LocalStorage;
            LocalStorageName = folderLocalStorage.Name;
            LocalStoragePath = folderLocalStorage.Path;
            Filename = "stockJson.txt";
            string output = "";
            try
            {
                output = DependencyService.Get<IFiles>().ReadTextFile(DirectoryName, Filename);
            }
            catch (Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    e.Message, "Ok");
            }
            return output;
            
        }

        public async Task WriteText(string text)
        {
            IFolder folderLocalStorage = FileSystem.Current.LocalStorage;
            LocalStorageName = folderLocalStorage.Name;
            LocalStoragePath = folderLocalStorage.Path;
            Filename = "stockJson.txt";
            try
            {
                DependencyService.Get<IFiles>().WriteTextFile(DirectoryName,
                    Filename,text);
            }
            catch (Exception e)
            {

                await Application.Current.MainPage.DisplayAlert("Error",
                e.Message, "Ok");
            }
        }



        public async Task<DateTime> GetLastWrite()
        {
            IFolder folderLocalStorage = FileSystem.Current.LocalStorage;
            LocalStorageName = folderLocalStorage.Name;
            LocalStoragePath = folderLocalStorage.Path;
            Filename = "stockJson.txt";
            DateTime dt = DateTime.Now;
            try
            {
                dt = DependencyService.Get<IFiles>().LastWriteDate(DirectoryName,
                    Filename);
            }
            catch (Exception e)
            {

                await Application.Current.MainPage.DisplayAlert("Error",
                e.Message, "Ok");
            }

            return dt;
        }

        public async Task<Boolean> FileExists()
        {
            IFolder folderLocalStorage = FileSystem.Current.LocalStorage;
            LocalStorageName = folderLocalStorage.Name;
            LocalStoragePath = folderLocalStorage.Path;
            Filename = "stockJson.txt";
            Boolean exists = false;
            try
            {
                exists =  DependencyService.Get<IFiles>().FileExists(DirectoryName,
                       Filename);
            }
            catch (Exception e)
            {

                await Application.Current.MainPage.DisplayAlert("Error",
                e.Message, "Ok");
            }

            return exists;
        }


        public ICommand WriteFileCommand { protected set; get; }
        public ICommand ReadFileCommand { protected set; get; }

    }
}
