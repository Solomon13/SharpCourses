using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic
{
    public class User : IUser
    {
        private string _name;
        private INotificationService _notificationService;

        public User(string userName, INotificationService notificationService)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException(nameof(userName));

            _name = userName;
            _notificationService = notificationService;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _notificationService.NotifyNameChanged(value);
                //Console.WriteLine($"Name was changed to {value}");
            }
        }
    }
}
