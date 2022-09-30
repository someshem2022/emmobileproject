

namespace EM.InsurePlus.Services.Navigation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using EM.InsurePlus.Services.Contacts;
    using EM.InsurePlus.ViewModels;
    using EM.InsurePlus.ViewModels.Base;
    using EM.InsurePlus.Views;
    using Xamarin.Forms;
    public partial class NavigationService : INavigationService
    {
       // private IAuthorizeService authorizationService;
       // private ISettingsService settingsService;
        protected readonly Dictionary<Type, Type> _mappings;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        //public NavigationService(IAuthorizeService authorizationService, ISettingsService settingsService)
        //{
        //    this.authorizationService = authorizationService;
        //    this.settingsService = settingsService;
        //    _mappings = new Dictionary<Type, Type>();
        //    CreatePageViewModelMappings();
        //}

        public NavigationService()
        {          
            _mappings = new Dictionary<Type, Type>();
            CreatePageViewModelMappings();
        }
        /// <summary>
        /// TO DO -->
        /// authorizationService.IsAuthenticated is temporary disabled and will be re-enabled when auth token is implemented
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            await NavigateToAsync<LoginViewModel>();



            //if (authorizationService.IsSessionNotExpired())
            //{
            //    await NavigateToAsync<MainViewModel>();
            //}
            //else
            //{
            //    if (settingsService.IsSettingsUpdated())
            //    {
            //        await NavigateToAsync<LoginViewModel>();
            //    }
            //    else
            //    {
            //        await NavigateToAsync<SettingsViewModel>();
            //    }
            //}
        }

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync(Type viewModelType)
        {
            return InternalNavigateToAsync(viewModelType, null);
        }

        public Task NavigateToAsync(Type viewModelType, object parameter)
        {
            return InternalNavigateToAsync(viewModelType, parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            var mainPage = CurrentApplication.MainPage as HomeView;

            if (mainPage != null)
            {
                mainPage.Navigation.RemovePage(
                    mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is LoginView)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                var navigationPage = mainPage.Detail as CustomNavigationPage;

                if (navigationPage != null)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                var navigationPage = CurrentApplication.MainPage as CustomNavigationPage;

                if (navigationPage != null)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        private Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            try
            {

            

            Type pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
            }
            catch (Exception ex)
            {

                var x = ex.ToString();
                return null;
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!_mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return _mappings[viewModelType];
        }

        private void CreatePageViewModelMappings()
        {
            _mappings.Add(typeof(RegisterViewModel), typeof(RegisterView));

            //Policy
            _mappings.Add(typeof(MainViewModel), typeof(MainView));
            _mappings.Add(typeof(HomeViewModel), typeof(HomeView));
            _mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            _mappings.Add(typeof(VehiclePolicyViewModel), typeof(VehiclePolicyView));
            _mappings.Add(typeof(VehicleOfflinePoliciesViewModel), typeof(VehicleOfflinePoliciesView));
        }
    }
}
