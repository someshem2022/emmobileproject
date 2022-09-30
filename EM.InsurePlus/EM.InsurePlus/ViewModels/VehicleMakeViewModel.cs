using EM.InsurePlus.Models.AppModels;
using EM.InsurePlus.ViewModels.Base;
using EM.InsurePlus.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace EM.InsurePlus.ViewModels
{
    public class VehicleMakeViewModel : PopupViewModelBase
    {
        public object ReturnValue;
        bool _canExecute = true;
        public ICommand VehicleTypeItemSelectedCommand => new Command(async (sender) => await OnVehicleSelected(sender));

        public ICommand ClosePopupTapCommand { get; private set; }

        private ObservableCollection<VehicleMakeModel> _vehicles;
        public ObservableCollection<VehicleMakeModel> Vehicles
        {
            get
            {
                return _vehicles;
            }
            set
            {
                _vehicles = value;
                OnPropertyChanged();
            }
        }
        public VehicleMakeViewModel()
        {
            GetVehicles();
            ClosePopupTapCommand =
         new Command(async () => await OnClosePopupTappedAsync(), () => _canExecute);
        }

        

        private void GetVehicles() 
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(VehicleMakePopupView)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("EM.InsurePlus.Files.Vehicles.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var rootobject = JsonConvert.DeserializeObject<VehicleRootObject>(json);
                var result = rootobject.makes;

                var vehicles = result.ToList();
                Vehicles = new ObservableCollection<VehicleMakeModel>(vehicles);
            }           
        }

        private async Task OnVehicleSelected(object e)
        {
            var item = (e as VehicleMakeModel);
            if (item != null)
            {
                ReturnValue = item;
            }
            await PopupNavigation.Instance.PopAsync();
        }
        private async Task OnClosePopupTappedAsync()
        {
            _canExecute = false;
            ((Command)ClosePopupTapCommand).ChangeCanExecute();

            await PopupNavigation.Instance.PopAsync();            
            _canExecute = true;
            ((Command)ClosePopupTapCommand).ChangeCanExecute();
        }
    }

   
}
