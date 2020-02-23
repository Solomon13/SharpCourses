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
            //var tester = new InheritanceTester();
            //tester.TestPolymorph();

            //// var abstractInstance = new AbstractClass(); -> imposible
            //var derive = new AbstractDerive();

            //var ex = new ExceptionsWorker();
            //ex.HandleException2();
            //ex.CatchByFinally();

            //var generic = new ClassWithGeneric();
            //Console.WriteLine($"Generic sum = { generic.Sum<int>(new int[] { 1, 2, 3 }, new Int32Adder())}");

            //var delegateTester = new WorkWithDelegates();
            //delegateTester.TestDelegates();
            //delegateTester.TestMulticast();

            //var eventTester = new WorkWithEventsTester();
            //eventTester.TestEvents();

            //var extr = new Extensions();
            //extr.TestSum();
            //extr.TestExtensions();

            //var linq = new LINQ();
            //linq.LinqDelayExecution();
            //Console.WriteLine("=========================");

            //linq.TestLinqMethod();


            //var reflectionTester = new TypeReflectionTester();
            //reflectionTester.DetectType();
            //reflectionTester.InvokeMethodNoArgs();
            //reflectionTester.InvokeMethodWithArgs();
            //reflectionTester.InvokeStatic();
            //reflectionTester.InvokePrivate();

            //var attributesTester = new AttributesTester();
            //attributesTester.TestClassAttribute();
            //attributesTester.TestFieldAttribute();

            //var assemblyTester = new DynamicLibraryTester();
            //assemblyTester.TestDynamicAssembly();

            //var t = new Threading();
            //t.ThreadsWithDelegates();
            //t.ThreadsWithDelegatesAndCallback();
            //t.NewThread();

            //var t1 = new ThreadSynchronization();
            //t1.ThreadsHellTest(enumSynchStateTester.Synchronization);

            //var t = new ThreadingExtendedTester();
            //t.TestDispatcher();
            //t.TestTreadPool();

            var t1 = new TasksTester();
            t1.TestAsyncWithException();

            Console.WriteLine("After call");

            Console.ReadLine();
        }
    }
}
