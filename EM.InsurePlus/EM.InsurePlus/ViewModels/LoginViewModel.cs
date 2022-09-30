using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EM.InsurePlus.Common;
using EM.InsurePlus.Services.Contacts;
using EM.InsurePlus.Validations;
using EM.InsurePlus.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EM.InsurePlus.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IUserService userService;
        private readonly IHelperService helperService;
        public ICommand SignUpCommand => new Command(() => OnSignUpAsync());

        public ICommand LoginCommand => new Command(() => OnLoginAsync());

        

        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
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

        public LoginViewModel(IUserService userService, IHelperService helperService)
        {
            this.userService = userService;
            this.helperService = helperService;
            UserName = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
        }
           
        public override Task InitializeAsync(object navigationData)
        {           
            return Task.FromResult(false);
        }

        private async Task OnSignUpAsync()
        {
            
             await NavigationService.NavigateToAsync<RegisterViewModel>(false);      
        }

        private bool ValidateUserName()
        {
            if (string.IsNullOrEmpty(UserName.Value))
            {
                UserName.IsValid = false;
                return false;
            }
            if (!helperService.CheckEmail(UserName.Value))
            {
                UserName.IsValid = false;
                return false;
            }

            UserName.IsValid = true;
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

        private bool ValidateUser() 
        {
            bool isValiduserName = ValidateUserName();
            bool isValidpassword = ValidatePassword();
            return isValiduserName && isValidpassword;


        }
        private async Task OnLoginAsync()
        {
            await NavigationService.NavigateToAsync<MainViewModel>();
            //if (!ValidateUser())
            //{
            //    await App.Current.MainPage.DisplayAlert("Login", "User Name or Password is empty", "OK");
            //    return;
            //}
            //IsBusy = true;
            //var result = await userService.AuthToken(new Models.AuthorizeModel { UserName = UserName.Value, Password = Password.Value });
            //IsBusy = false;
            //if (!result)
            //{
            //    await App.Current.MainPage.DisplayAlert("Login", "Login Failed", "OK");
            //    return;
            //}
            //else
            //{

            //    await NavigationService.NavigateToAsync<MainViewModel>();

            //}



        }

        
    }
}
