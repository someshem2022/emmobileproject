using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using EM.InsurePlus.Services.Contacts;
using EM.InsurePlus.Validations;
using EM.InsurePlus.ViewModels.Base;
using Xamarin.Forms;

namespace EM.InsurePlus.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public ICommand RegisterCommand => new Command(() => OnRegisterAsync());
        private readonly IUserService userService;
        private readonly IHelperService helperService;


        private string _address = string.Empty;
        private string _licencePlate = string.Empty;
        private ValidatableObject<string> _name;
        private ValidatableObject<string> _mobile;
        private ValidatableObject<string> _email;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _confirmPassword;
        private ValidatableObject<string> _phone;

        public ValidatableObject<string> Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

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
        public ValidatableObject<string>ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject<string> Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }


        public ValidatableObject<string> Mobile
        {
            get
            {
                return _mobile;
            }
            set
            {
                _mobile = value;
                OnPropertyChanged();
            }
        }


        public string licencePlate
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

        public string Address
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


        public RegisterViewModel(IUserService userService, IHelperService helperService)
        {
            this.userService = userService;
            this.helperService = helperService;

            Name = new ValidatableObject<string>();
            Mobile = new ValidatableObject<string>();
            Phone = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            ConfirmPassword = new ValidatableObject<string>();
            //Email.Validations.Add(new EmailRule());
        }

        public override Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }

        private bool ValidateName() 
        {
            if (string.IsNullOrEmpty(Name.Value))
            {
                Name.IsValid = false;
                return false;
            }

            Name.IsValid = true;
            return true;
        }


        private bool ValidateEmail()
        {
            if (string.IsNullOrEmpty(Email.Value))
            {
                Email.IsValid = false;
                return false;
            }
           
            if (!helperService.CheckEmail(Email.Value))
            {
                Email.IsValid = false;
                return false;
            }

            Email.IsValid = true;
            return true;
        }

      
        private bool ValidatePhone()
        {
            if (string.IsNullOrEmpty(Phone.Value))
            {
                Phone.IsValid = false;
                return false;
            }

            Phone.IsValid = true;
            return true;
        }
        private bool ValidatePassword()
        {
            if (string.IsNullOrEmpty(Password.Value))
            {
                Password.IsValid = false;
                return false;
            }

            Password.IsValid = true;
            return true;
        }
        private bool ValidateConfirmPassword()
        {
            if (string.IsNullOrEmpty(ConfirmPassword.Value))
            {
                ConfirmPassword.IsValid = false;
                return false;
            }

            ConfirmPassword.IsValid = true;
            return true;
        }


        private bool ValidateUser() 
        {
            var isNameValid = ValidateName();
            var isEmailValid = ValidateEmail();
            var isPhoneValid = ValidatePhone();
            var isPasswordValid = ValidatePassword();
            var isConfirmPasswordValid = ValidateConfirmPassword();

            return isNameValid && isEmailValid && isPhoneValid && isPasswordValid && isConfirmPasswordValid;
        }
        private async Task OnRegisterAsync()
        {
            //if (string.IsNullOrEmpty(Email.Value) || string.IsNullOrEmpty(Phone.Value) ||
            //    string.IsNullOrEmpty(Password.Value) || string.IsNullOrEmpty(ConfirmPassword.Value)) 
            //{

            //    return;

            //}
            if (!ValidateUser()) return;
            if (Password.Value != ConfirmPassword.Value)
            {
                await App.Current.MainPage.DisplayAlert("Register", "Password and Confrim Password are not same", "OK");
                return;
            }


            var model = new Models.User { Email = Email.Value, Password = Password.Value, PhoneNumber = Phone.Value };

            var result = await userService.Register(model);
            if (result)
            {
                await App.Current.MainPage.DisplayAlert("Register", "Successfully Registered", "OK");
                await NavigationService.NavigateBackAsync();

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Register", "Failed To Register", "OK");
            }

        }
    }
}
