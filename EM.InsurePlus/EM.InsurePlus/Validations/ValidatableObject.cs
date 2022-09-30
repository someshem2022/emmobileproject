using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace EM.InsurePlus.Validations
{
    public class ValidatableObject<T> : BindableObject, IValidity
    {
        private readonly List<IValidationRule<T>> _validations;
        private readonly ObservableCollection<string> _errors;
        private T _value;
        private bool _isValid;
        private bool _isDefaultState;

        public List<IValidationRule<T>> Validations => _validations;

        public ObservableCollection<string> Errors => _errors;

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }

            set
            {
                _isValid = value;
                OnPropertyChanged();
            }
        }

        public bool IsDefaultState
        {
            get
            {
                return _isDefaultState;
            }

            set
            {
                _isDefaultState = value;
                OnPropertyChanged();
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _isDefaultState = true;
            _errors = new ObservableCollection<string>();
           // _validations = new List<IValidationRule<T>>();
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                                                     .Select(v => v.ValidationMessage);

            foreach (var error in errors)
            {
                Errors.Add(error);
            }

            IsValid = !Errors.Any();

            return this.IsValid;
        }
    }
}
