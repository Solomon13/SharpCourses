using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model;

namespace WpfApp1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<User> _users = new ObservableCollection<User>
        {
            new User("Oleg", "Orlov", enumGender.Male),
            new User("Sasha", "Orlov", enumGender.Male),
            new User("Olga", "Orlova", enumGender.Female)
        };

        public ObservableCollection<User> Users => _users;

        private RelayCommand _addUserCommand;

        public RelayCommand AddUserCommand
        {
            get
            {
                return _addUserCommand ?? (_addUserCommand = new RelayCommand(
                obj =>
                {
                    var vm = new RegistrationViewModel();
                    var registrationWindow = new RegistrationWindow
                    {
                        DataContext = vm
                    };

                    if(registrationWindow.ShowDialog() == true)
                    {
                        Users.Add(new User(vm.FirstName, vm.LastName, vm.SelectedGender));
                    }

                },
                obj =>
                {
                    return true;
                }
                ));
            }
        }

    }
}
