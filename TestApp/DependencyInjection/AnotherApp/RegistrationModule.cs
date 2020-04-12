using Autofac;
using BusinesLogic;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AnotherApp.Program;

namespace AnotherApp
{
    class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageBoxNotification>().As<INotificationService>().SingleInstance();
            builder.RegisterType<MemoryUsersService>().As<IUsersService>().SingleInstance();
            builder.RegisterType<User>().As<IUser>();
            base.Load(builder);
        }
    }
}
