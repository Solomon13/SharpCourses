using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private double _textBoxHeight;
        //private bool _textBoxVisibility;

        public RegistrationViewModel()
        {
            //_firstName = "Sasha";
            //_lastName = "Orlov";
            _textBoxHeight = 30;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
