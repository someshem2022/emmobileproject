﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EM.InsurePlus.ViewModels.Base;

namespace EM.InsurePlus.Services.Contacts
{
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task NavigateToAsync(Type viewModelType);

        Task NavigateToAsync(Type viewModelType, object parameter);

        Task NavigateBackAsync();

        Task RemoveLastFromBackStackAsync();

        //Task NavigateToPopupAsync<TViewModel>(bool animate) where TViewModel : ViewModelBase;

       // Task NavigateToPopupAsync<TViewModel>(object parameter, bool animate) where TViewModel : ViewModelBase;
    }
}
