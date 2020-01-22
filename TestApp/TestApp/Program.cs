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
            var t1 = new GCTester();

            t1.TestManaged();
            t1.TestUnmanaged();

            GC.Collect();
            GC.WaitForPendingFinalizers();


            var t2 = new DisposableObjectTester();
            t2.TestDispose();
            t2.TestDisposeUsing();
        }
    }
}
