

namespace EM.InsurePlus.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using EM.InsurePlus.ViewModels.Base;
    using Xamarin.Forms;
    public class MenuViewModel : ViewModelBase
    {
        //private readonly IAuthorizeService authorizationService;

        private ObservableCollection<Models.AppModels.MenuItem> _menuItems;

        public ICommand MenuSelectedCommand => new Command(async (sender) => await OnMenuSelectedAsync(sender));

        //public string UserName => Settings.OfficerName;

       // public string ClientName => Settings.ClientName;

        //public string AppServcieUrl => Settings.AppServcieUrl;

        public MenuViewModel()
        {
            //this.authorizationService = authorizationService;
            //AppVersion = $"Version {VersionTracking.CurrentVersion}";
            MenuItems = new ObservableCollection<Models.AppModels.MenuItem>();
        }

        public ObservableCollection<Models.AppModels.MenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        //private Task RemoveUserCredentials()
        //{
        //    //return authorizationService.LogoutAsync();
        //}

        private async Task OnMenuSelectedAsync(object e)
        {
            if (e != null)
            {
                switch (e.ToString())
                {
                    case "1":
                       // await NavigationService.NavigateToAsync(typeof(ZplTemplateViewModel));
                        break;

                    case "2":
                        //await RemoveUserCredentials();
                        //await NavigationService.NavigateToAsync(typeof(LoginViewModel));
                        break;

                    case "3":
                        //await NavigationService.NavigateToAsync(typeof(AppSettingsViewModel));
                        break;
                }
            }
        }

        public Task OnViewAppearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }
    }
}
