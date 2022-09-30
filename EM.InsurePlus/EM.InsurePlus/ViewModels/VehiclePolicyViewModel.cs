using AutoMapper;
using EM.InsurePlus.Data.Repositories.Contracts;
using EM.InsurePlus.Models.AppModels;
using EM.InsurePlus.Services.Contacts;
using EM.InsurePlus.Validations;
using EM.InsurePlus.ViewModels.Base;
using EM.InsurePlus.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using SO = EM.InsurePlus.Models.ServiceModels;

namespace EM.InsurePlus.ViewModels
{
    using DO = Models.DataModels;
    public class VehiclePolicyViewModel : ViewModelBase
    {
        private readonly IVehiclePolicyRepository vehiclePolicyRepository;
        private readonly IPolicyService policyService;       
        private ValidatableObject<string> _firstName;
        private ValidatableObject<string> _lastName;
        private ValidatableObject<string> _nic;
        private ValidatableObject<string> _address;
        private ValidatableObject<string> _make;
        private ValidatableObject<string> _model;
        private ValidatableObject<string> _color;
        private ValidatableObject<string> _year;
        private ValidatableObject<string> _licencePlate;
        private ValidatableObject<string> _phone;
        public ImageSource _imgPickerSource1 = "add_image.png";
        public byte[] _image1Bytes = null;
        public ImageSource ImgPickerSource1
        {
            get => _imgPickerSource1;
            set
            {
                if (_imgPickerSource1 == value) return;
                _imgPickerSource1 = value;
                RaisePropertyChanged(() => ImgPickerSource1);
            }
        }
        public ICommand AddImageCommand => new Command(async () => await OnTakePhoto());
        public ICommand SubmitPolicyCommand => new Command(async () => await OnSubmitPolicyAsync());

        public ICommand OfflinePoliciesCommand => new Command(async () => await OnOfflinePolicies());
        public ICommand OpenMakeCommand => new Command(async () => await OnMakeOpen());

        public ICommand OpenColorCommand => new Command(async () => await OnColorOpen());
        public ICommand ClearCommand => new Command(() =>  OnClear());
               

        public ValidatableObject<string> Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Nic
        {
            get
            {
                return _nic;
            }
            set
            {
                _nic = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> LicenePlate
        {
            get
            {
                return _licencePlate;
            }
            set
            {
                _licencePlate = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Year
        {
            get
            {
                return _year;
            }
            set
            {
                _year = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }
        public ValidatableObject<string> Make
        {
            get
            {
                return _make;
            }
            set
            {
                _make = value;
                OnPropertyChanged();
            }
        }

        public VehiclePolicyViewModel(IVehiclePolicyRepository vehiclePolicyRepository,
            IPolicyService policyService)
        {
            this.vehiclePolicyRepository = vehiclePolicyRepository;
            this.policyService = policyService;

            FirstName = new ValidatableObject<string>();
            LastName = new ValidatableObject<string>();
            Make = new ValidatableObject<string>();
            Model = new ValidatableObject<string>();
            Color = new ValidatableObject<string>();
            Nic = new ValidatableObject<string>();
            Address = new ValidatableObject<string>();
            LicenePlate = new ValidatableObject<string>();
            Phone = new ValidatableObject<string>();           
           
        }
        public override async Task InitializeAsync(object navigationData)
        {
            Make.Value = "Make";
            Color.Value = "Color";
        }
        
        async Task OnTakePhoto()
        {          
            var result = await Xamarin.Essentials.MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                
                _image1Bytes = ConvertToByte(stream);
                ImgPickerSource1 = ConvertToImage(_image1Bytes);
            }
        }

        private async Task OnSubmitPolicyAsync()
        {
            if (!ValidatePolicy()) return;

            var model = new DO.VehiclePolicy()
            {
                FirstName = FirstName.Value,
                LastName = LastName.Value,
                Address = Address.Value,
                Nic = Nic.Value,                
                LicencePlate  = LicenePlate.Value,
                Make = Make.Value,
                Model = Model.Value,
                Color = Color.Value,               
                Image = _image1Bytes != null? Convert.ToBase64String(_image1Bytes):string.Empty
            };

           
           

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await SaveOfflineAsync(model);
            }
            else
            {
                var mappedModel = Mapper.Map<SO.VehiclePolicy>(model);
                var result = await policyService.CreatePolicy(mappedModel);
                if (result!= null)
                {
                    await Application.Current.MainPage.DisplayAlert("Save Vehicle Policy", "Succesfully Saved Online", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Save Vehicle Policy", "An Error occured!", "OK");
                }

            }
            
           
        }
        private static byte[] ConvertToByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private ImageSource ConvertToImage(byte[] value)
        {
            ImageSource retSource = null;
            if (value != null)
            {
                byte[] imageAsBytes = (byte[])value;
                retSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            }
            return retSource;
        }

        private bool ValidatePolicy()
        {
            var isValidFirstName = ValidateFirstName();
            var isValidLastName = ValidateLastName();
            var isValidNic = ValidateNic();
            var isValidAddress = ValidateAddress();
            var isValidLicencePlate = ValidateLicencePlace();
            var isValidMake = ValidateMake();
            var isValidColor = ValidateColor();
            var isValidMode = ValidateModel();
            return isValidFirstName && isValidLastName && isValidNic && 
                isValidAddress && isValidLicencePlate && isValidMake && isValidColor && isValidMode;
        }
       private async Task SaveOfflineAsync(DO.VehiclePolicy model) 
        {
            var save = await vehiclePolicyRepository.Save(model);

            if (!save)
            {
                await Application.Current.MainPage.DisplayAlert("Save Vehicle Policy", "An Error occured whiile saving Policy", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Save Vehicle Policy", "Succesfully Saved Policy", "OK");
            }
        }
        private async Task OnOfflinePolicies()
        {
            await NavigationService.NavigateToAsync<VehicleOfflinePoliciesViewModel>(true);
        }

        private bool ValidateFirstName()
        {
            if (string.IsNullOrEmpty(FirstName.Value))
            {
                FirstName.IsValid = false;
                return false;
            }

            FirstName.IsValid = true;
            return true;
        }

        private bool ValidateLastName()
        {
            if (string.IsNullOrEmpty(LastName.Value))
            {
                LastName.IsValid = false;
                return false;
            }

            LastName.IsValid = true;
            return true;
        }

        private bool ValidateNic()
        {
            if (string.IsNullOrEmpty(Nic.Value))
            {
                Nic.IsValid = false;
                return false;
            }

            Nic.IsValid = true;
            return true;
        }

        private bool ValidateAddress()
        {
            if (string.IsNullOrEmpty(Address.Value))
            {
                Address.IsValid = false;
                return false;
            }

            Address.IsValid = true;
            return true;
        }

        private bool ValidateLicencePlace()
        {
            if (string.IsNullOrEmpty(LicenePlate.Value))
            {
                LicenePlate.IsValid = false;
                return false;
            }

            LicenePlate.IsValid = true;
            return true;
        }

        private bool ValidateMake()
        {
            if (string.IsNullOrEmpty(Make.Value) || Make.Value == "Make")
            {
                Make.IsValid = false;
                return false;
            }

            Make.IsValid = true;
            return true;
        }

        private bool ValidateColor()
        {
            if (string.IsNullOrEmpty(Color.Value) || Color.Value == "Color")
            {
                Color.IsValid = false;
                return false;
            }

            Color.IsValid = true;
            return true;
        }

        private bool ValidateModel()
        {
            if (string.IsNullOrEmpty(Model.Value))
            {
                Model.IsValid = false;
                return false;
            }

            Model.IsValid = true;
            return true;
        }

        private async Task OnMakeOpen()              
        {
            var popup = new VehicleMakePopupView();
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);

            var result = await popup.PopupClosedTask;
            if (result == null) return;

            var vehicle = (result as VehicleMakeModel);
            if (vehicle == null) return;
            Make.Value = vehicle.Name;
        }
        private async Task OnColorOpen()
        {
            var popup = new VehicleColorPopupView();
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(popup);

            var result = await popup.PopupClosedTask;
            if (result == null) return;

            var color = (result as VehicleColorModel);
            if (color == null) return;
            Color.Value = color.Name;
        }

        private void OnClear()
        {
            FirstName.Value = string.Empty;
            LastName.Value = string.Empty;
            Address.Value = string.Empty;
            Nic.Value = string.Empty;
            LicenePlate.Value = string.Empty;
            Make.Value = "Make";
            Color.Value = "Color";
            Model.Value = string.Empty;
            ImgPickerSource1 = null;           
        }










































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































    }
}
