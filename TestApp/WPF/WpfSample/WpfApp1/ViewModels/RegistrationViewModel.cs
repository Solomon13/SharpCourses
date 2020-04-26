﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private string _firstName;
        private string _lastName;
        private double _textBoxHeight;
        private enumGender[] _genders = Enum.GetValues(typeof(enumGender)) as enumGender[];
        private enumGender _selectedGender = enumGender.Male;

        public RegistrationViewModel()
        {
        }

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged(nameof(FirstName));
                RaisePropertyChanged(nameof(TextBoxVisibility));
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
            }
        }

        public double TextBoxHeight
        {
            get { return _textBoxHeight; }
        }

        public bool TextBoxVisibility
        {
            get { return !string.IsNullOrEmpty(_firstName); }
        }

        public enumGender[] Genders
        {
            get
            {
                return _genders;
            }
        }

        public enumGender SelectedGender
        {
            get { return _selectedGender; }
            set
            {
                if(_selectedGender != value)
                {
                    _selectedGender = value;
                    RaisePropertyChanged(nameof(SelectedGender));
                }
            }
        }
    }
}
