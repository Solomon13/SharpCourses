using Autofac;
using BusinesLogic;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection
{
    class Program
    {
        static void Main(string[] args)
        {
            //configuration
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleNotificationService>().As<INotificationService>().SingleInstance();
            builder.RegisterType<MemoryUsersService>().As<IUsersService>().SingleInstance();
            builder.RegisterType<User>().As<IUser>();
            //Build
            var container = builder.Build();

            var userService = container.Resolve<IUsersService>(new TypedParameter(typeof(IContainer), container));
            var user = userService.CreateUser("Tolya");

            user.Name = "Vasya";

            Console.ReadLine();
        }

        

        public class ConsoleNotificationService : INotificationService
        {
            public void NotifyNameChanged(string newName)
            {
                Console.WriteLine($"Name was changed to {newName}");
            }
        }
    }
}
