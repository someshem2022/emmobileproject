using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EM.InsurePlus.ViewModels.Base
{
    public class PopupViewModelBase : ExtendedBindableObject
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public PopupViewModelBase()
        {

        }

        public virtual Task InitializeAsync()
        {
            return Task.FromResult(false);
        }

    }
}
