using AutoMapper;
using EM.InsurePlus.Data.Repositories.Contracts;
using EM.InsurePlus.Services.Contacts;
using EM.InsurePlus.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EM.InsurePlus.ViewModels
{
    using DO = Models.DataModels;
    using SO = Models.ServiceModels;
    public class VehicleOfflinePoliciesViewModel : ViewModelBase
    {
        private readonly IVehiclePolicyRepository vehiclePolicyRepository;
        private readonly IPolicyService policyService;
        private ObservableCollection<SO.VehiclePolicy> _policies;
        public ObservableCollection<SO.VehiclePolicy> Policies
        {
            get
            {
                return _policies;
            }
            set
            {
                _policies = value;
                OnPropertyChanged();
            }
        }

        public ICommand SyncDataCommand => new Command(async () => await OnSyncData());

       

        public VehicleOfflinePoliciesViewModel(IVehiclePolicyRepository vehiclePolicyRepository,
            IPolicyService policyService)
        {
            this.vehiclePolicyRepository = vehiclePolicyRepository;
            this.policyService = policyService;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            await GetDataAsync();
        }

        private async Task GetDataAsync() 
        {
            var data =  await vehiclePolicyRepository.Get();
            Policies = new ObservableCollection<SO.VehiclePolicy>(data);
        }

        private async Task OnSyncData()
        {
            var data = await vehiclePolicyRepository.Get();           
           
            foreach (var item in data)
            {
                var result = await policyService.CreatePolicy(item);
            }
        }
    }
}
