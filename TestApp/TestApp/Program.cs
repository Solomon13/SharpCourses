
using ExtendedSharp;
using OopSharp;
using ReferenceLib;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkWithDatabase;
using WorkWithEntityFramework;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            DbInterception.Add(new LogFormatter());

            RunEntity();
            //RunAdoNet();
            Console.ReadLine();
        }

        
        private static void RunAdoNet()
        {
            var tester = new AdoNetTester();
            var task = tester.TaskTestDbWithAsync();

            while (!task.IsCompleted)
            {
                Console.WriteLine("main tread still alive");
                Thread.Sleep(50);
            }

            var oldT = tester.TestDbWithThread();

            while (oldT.IsAlive)
            {
                Console.WriteLine("Main tread still alive");
                Thread.Sleep(50);
            }
        }

        private static void RunEntity()
        {
            var entityTester = new EntityTester();
            entityTester.WorkWithComplexRequest();
        }
    }
}
