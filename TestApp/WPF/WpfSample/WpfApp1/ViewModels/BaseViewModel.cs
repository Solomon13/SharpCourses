using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void RaisePropertisChanged(string[] propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
