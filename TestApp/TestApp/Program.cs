using ExtendedSharp;
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

            var generic = new ClassWithGeneric();
            Console.WriteLine($"Generic sum = { generic.Sum<int>(new int[] { 1, 2, 3 }, new Int32Adder())}");

            var delegateTester = new WorkWithDelegates();
            delegateTester.TestDelegates();
            delegateTester.TestMulticast();

            var eventTester = new WorkWithEventsTester();
            eventTester.TestEvents();

            var extr = new Extensions();
            extr.TestSum();
            extr.TestExtensions();

            Console.ReadLine();
        }
    }
}
