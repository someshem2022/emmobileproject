﻿using EM.InsurePlus.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EM.InsurePlus.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleMakePopupView : PopupPage
    {
        private TaskCompletionSource<object> _taskCompletionSource;
        public Task<object> PopupClosedTask => _taskCompletionSource.Task;
        public VehicleMakePopupView()
        {
            InitializeComponent();
            BindingContext = new VehicleMakeViewModel();
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _taskCompletionSource = new TaskCompletionSource<object>();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
           _taskCompletionSource.SetResult(((VehicleMakeViewModel)BindingContext).ReturnValue);
        }
    }
}