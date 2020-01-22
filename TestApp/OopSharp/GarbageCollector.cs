using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public class GarbageCollectorTester
    {
        private const int ArrSize = 100000000;
        private int[] VeryBigArray = new int[ArrSize];
        private IntPtr UnmanagedArray;

        public void InitManaged()
        {
            var rnd = new Random();

            for (int i = 0; i < ArrSize; i++)
            {
                VeryBigArray[i] = rnd.Next(100);
            }
        }

        public unsafe void InitUnmanaged()
        {
            var unmanagedArray = (int*) Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * ArrSize).ToPointer();
            var rnd = new Random();

            for (int i = 0; i < ArrSize; i++)
            {
                *(unmanagedArray + i) = rnd.Next(100);
            }

            UnmanagedArray = new IntPtr(unmanagedArray);
        }

        //Used often to clear unmanaged resources
        ~GarbageCollectorTester()
        {
            unsafe //unsafe operations
            {
                if (UnmanagedArray != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(UnmanagedArray);
                    UnmanagedArray = IntPtr.Zero;
                }
            }
        }

        public void CollectManaged(int gen)
        {
            VeryBigArray = null;
            GC.Collect(gen);
            GC.WaitForPendingFinalizers();
        }
    }

    public class GCTester
    {
        public void TestManaged()
        {
            var t = new GarbageCollectorTester();

            t.InitManaged();
            t.CollectManaged(0); //will not clear the array
            t.CollectManaged(2); //will clear the arrau
        }

        public void TestUnmanaged()
        {
            var t = new GarbageCollectorTester();

            t.InitUnmanaged();
        }
    }
}
