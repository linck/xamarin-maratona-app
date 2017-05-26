using MonkeyHubApp.Helpers;
using MonkeyHubApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MonkeyHubApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly AzureService _azureService;
        //INavigation navigation;

        private bool _isBusy;

        ICommand _loginCommand;

        public ICommand LogonCommand => 
            _loginCommand ?? (_loginCommand = new Command(async () => await ExecuteLoginCommandAsync()));


        public LoginViewModel()
        {
            _azureService = DependencyService.Get<AzureService>();
            //navigation = nav;

            Title = "Social Login Demo";
        }

        private async Task ExecuteLoginCommandAsync()
        {
            if (_isBusy || !(await LoginAsync()))
            {
                return;
            }
            else
            {
                //var mainPage = new MainPage();
                //await _navigation.PushAsync(mainPage);
                await PushAsync<MainViewModel>();

                //RemovePageFromStack();
            }
            _isBusy = false;
        }

        //private void RemovePageFromStack()
        //{
        //    var existingPages = _navigation.NavigationStack;
        //    foreach (var page in existingPages)
        //    {
        //        _navigation.RemovePage(page);
        //    }
        //}

        private Task<bool> LoginAsync()
        {
            if (Settings.IsLoggedIn)
                return Task.FromResult(true);

            return _azureService.LoginAsync();
        }
    }
}
