using Autofac;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLogic
{
    public class MemoryUsersService : IUsersService
    {
        public MemoryUsersService(IContainer container)
        {
            _container = container;

            _notificationService = _container.Resolve<INotificationService>();
        }

        public IUser CreateUser(string name)
        {
            var user = _container.Resolve<IUser>(new TypedParameter(typeof(string), name), 
                                                 new TypedParameter(typeof(INotificationService), _notificationService)); /*new User(name, _notificationService);*/
            _users.Add(user);

            return user;
        }

        private List<IUser> _users = new List<IUser>();
        private INotificationService _notificationService;
        private IContainer _container;
    }
}
