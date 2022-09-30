

namespace EM.InsurePlus.ViewModels.Base
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using EM.InsurePlus.Services.Contacts;
    using Xamarin.Essentials;

    public class ViewModelBase : ExtendedBindableObject
    {
        protected readonly INavigationService NavigationService;
        private bool _isBusy;
        private bool _isConnectivityMessageVisibile;
        public bool IsConnectivityMessageVisibile
        {
            get => _isConnectivityMessageVisibile;
            set
            {
                if (_isConnectivityMessageVisibile == value) return;
                _isConnectivityMessageVisibile = value;
                OnPropertyChanged();
            }
        }
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public ViewModelBase()
        {
            NavigationService = Locator.Instance.Resolve<INavigationService>();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            //IsConnected = (Connectivity.NetworkAccess == NetworkAccess.Internet);

            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                //InternetImage = "internet_connected.png";
                //ToastConfig toastConfig = new ToastConfig("");
                //toastConfig.SetBackgroundColor(Color.Transparent);
                //toastConfig.SetPosition(ToastPosition.Top);
                //UserDialogs.Instance.Toast(toastConfig).Dispose();
                IsConnectivityMessageVisibile = false;
            }
            else
            {
               //InternetImage = "internet_disconnected.png";
                IsConnectivityMessageVisibile = true;
            }
        }
    }
}
