using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EM.InsurePlus.ViewModels.Base;
using Xamarin.Forms;

namespace EM.InsurePlus.ViewModels
{
    public  class HomeViewModel :ViewModelBase
    {
        public ICommand VehiclePolicyCommand => new Command(async () => await OnVehiclePolicy());



        //public ICommand RegisterVehicleCommand => new Command(() => OnRegisterVehicleAsync());

        //public ICommand MyParkingCommand => new Command(() => OnMyParkingAsync());

      
        public HomeViewModel()
        {

        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }
        private async Task OnVehiclePolicy()
        {
            await NavigationService.NavigateToAsync<VehiclePolicyViewModel>(true);
        }
        //private async Task OnRegisterVehicleAsync()
        //{
        //    await NavigationService.NavigateToAsync<RegisterVehicleViewModel>(true);
        //}

        //private async Task OnMyParkingAsync()
        //{
        //    await NavigationService.NavigateToAsync<MyParkingViewModel>(true);
        //}

    }
}
