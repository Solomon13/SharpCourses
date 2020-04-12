using Autofac;
using BusinesLogic;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnotherApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //configuration
            var builder = new ContainerBuilder();
            builder.RegisterModule<RegistrationModule>();

            //Build
            var container = builder.Build();

            var userService = container.Resolve<IUsersService>(new TypedParameter(typeof(IContainer), container));
            var user = userService.CreateUser("Tolya");

            user.Name = "Vasya";

            Console.ReadLine();
        }

        public class MessageBoxNotification : INotificationService
        {
            public void NotifyNameChanged(string newName)
            {
                MessageBox.Show($"New name {newName}");
            }
        }
    }
}
