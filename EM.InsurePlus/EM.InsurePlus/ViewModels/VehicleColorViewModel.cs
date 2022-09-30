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
    public class VehicleColorViewModel : PopupViewModelBase
    {
        public object ReturnValue;
        bool _canExecute = true;

        public ICommand ColorItemSelectedCommand => new Command(async (sender) => await OnColorSelected(sender));               

        public ICommand ClosePopupTapCommand { get; private set; }

        private ObservableCollection<VehicleColorModel> _colors;
        public ObservableCollection<VehicleColorModel> Colors
        {
            get
            {
                return _colors;
            }
            set
            {
                _colors = value;
                OnPropertyChanged();
            }
        }
        public VehicleColorViewModel()
        {
            GetColors();
            ClosePopupTapCommand =
        new Command(async () => await OnClosePopupTappedAsync(), () => _canExecute);

        }
        private void GetColors()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(VehicleColorPopupView)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("EM.InsurePlus.Files.Colors.json");

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                var rootobject = JsonConvert.DeserializeObject<VehicleColorObject>(json);
                var result = rootobject.colors;

                var colors = result.ToList();
                Colors = new ObservableCollection<VehicleColorModel>(colors);
            }
        }

        private async Task OnColorSelected(object e)
        {
            var item = (e as VehicleColorModel);
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
