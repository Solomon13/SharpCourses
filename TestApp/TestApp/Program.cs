using OopSharp;
using ReferenceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var tester = new InheritanceTester();
            tester.TestPolymorph();

            // var abstractInstance = new AbstractClass(); -> imposible
            var derive = new AbstractDerive();

            var ex = new ExceptionsWorker();
            ex.HandleException2();
            ex.CatchByFinally();

            Console.ReadLine();
        }
    }
}
