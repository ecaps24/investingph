using GalaSoft.MvvmLight.Views;
using investingph.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace investingph.Services
{
    public class NavigationService : INavigationService
    {
        RootViewModel root = new RootViewModel();
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }




        public void Initialize(NavigationPage navigationPage)
        {
            _navigation = navigationPage;
            Title = "Navigation";
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }



        public async Task GoBack()
        {
            if (CanGoBack())
            {
                await _navigation.PopAsync();
            }
        }

        public async Task GoHome()
        {
            if (CanGoBack())
            {
                await _navigation.PopToRootAsync();

            }


        }

        public bool CanGoBack()
        {
            return _navigation?.Navigation?.NavigationStack?.Count > 1;
        }

        public async Task NavigateTo(string pageKey)
        {
            await NavigateTo(pageKey, null);
        }

        // Required for interface
        public async Task NavigateTo(string pageKey, object parameter)
        {
            await NavigateTo(pageKey, parameter, null, null, null, null, null, false);
        }

        // Two or more parameters
        public async Task NavigateTo(string pageKey, object parameter1, object parameter2, object parameter3 = null,
                               object parameter4 = null, object parameter5 = null, object parameter6 = null)
        {
            await NavigateTo(pageKey, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6, false);
        }

        private async Task NavigateTo(string pageKey, object parameter1, object parameter2, object parameter3,
                                object parameter4, object parameter5, object parameter6, bool replaceRoot)
        {
            //  lock (_pagesByKey)
       
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];
                    ConstructorInfo constructor;
                    List<object> p = new List<object>();
                    if (parameter1 != null)
                        p.Add(parameter1);
                    if (parameter2 != null)
                        p.Add(parameter2);
                    if (parameter3 != null)
                        p.Add(parameter3);
                    if (parameter4 != null)
                        p.Add(parameter4);
                    if (parameter5 != null)
                        p.Add(parameter5);
                    if (parameter6 != null)
                        p.Add(parameter6);
                    object[] parameters = p.ToArray();
                    constructor = GetConstructor(type, parameters);
                    if (constructor == null)
                    {
                        var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                        throw new InvalidOperationException(exceptionMessage);
                    }
                    if (!replaceRoot)
                    {
                        try
                        {
                            var page = constructor.Invoke(parameters) as Page;
                            await _navigation.PushAsync(page, false);
                            
                        }
                        catch (Exception e)
                        {

                            Debug.WriteLine(e.Message);
                        }


                    }
                    else
                    {
                        var page = constructor.Invoke(parameters) as Page;
                        var root = _navigation.Navigation.NavigationStack.First();
                        _navigation.Navigation.InsertPageBefore(page, root);
                        await _navigation.PopToRootAsync(false);
                    }
                }
                else
                {
                    var exceptionMessage = $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                    throw new ArgumentException(exceptionMessage, nameof(pageKey));
                }
            }
        }

        private ConstructorInfo GetConstructor(Type type, object[] parameters)
        {
            var parameterCount = parameters.Length;
            ConstructorInfo constructor;
            if (parameterCount > 0)
            {
                constructor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(
                c => {
                    var p = c.GetParameters();
                    return p.Count() == parameterCount && p[parameterCount - 1].ParameterType == parameters[parameterCount - 1].GetType();
                });
            }
            else
            {
                constructor = type.GetTypeInfo()
                                .DeclaredConstructors
                                .FirstOrDefault(c => !c.GetParameters().Any());

            }
            return constructor;
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public async Task SetNewRoot(string pageKey)
        {
            await NavigateTo(pageKey, null, null, null, null, null, null, replaceRoot: true);
        }

        void INavigationService.NavigateTo(string pageKey)
        {
            throw new NotImplementedException();
        }

        void INavigationService.NavigateTo(string pageKey, object parameter)
        {
            throw new NotImplementedException();
        }

        void INavigationService.GoBack()
        {
            throw new NotImplementedException();
        }
    }
}
