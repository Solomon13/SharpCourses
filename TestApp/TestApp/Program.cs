using ExtendedSharp;
using OopSharp;
using ReferenceLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkWithDatabase;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
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

            Console.ReadLine();
        }

        

    }
}
