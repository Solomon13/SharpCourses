using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    //use IDisposable for manual resources releasing
    public class DisposableObject : IDisposable
    {
        private const int ArrSize = 100000000;
        private IntPtr UnmanagedArray;

        public unsafe void InitUnmanaged()
        {
            var unmanagedArray = (int*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * ArrSize).ToPointer();
            var rnd = new Random();

            for (int i = 0; i < ArrSize; i++)
            {
                *(unmanagedArray + i) = rnd.Next(100);
            }

            UnmanagedArray = new IntPtr(unmanagedArray);
        }

        public void Dispose()//perform resources release here. GC will not call it. Do it manualy
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
    }

    public class DisposableObjectTester
    {
        //both methods will do the same
        public void TestDispose()
        {
            var t = new DisposableObject();
            
            try
            {
                t.InitUnmanaged();
            }
            finally
            {
                t.Dispose();
            }
        }

        public void TestDisposeUsing()
        {
            //using = try{...}finally{obj.Dispose()}
            using(var t = new DisposableObject())
            {
                t.InitUnmanaged();
            }
        }
    }
}
