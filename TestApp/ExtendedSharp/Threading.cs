using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class Threading
    {
        public void AccessToMainThread()
        {
            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "ThePrimaryThread";

            Console.WriteLine("Name of current AppDomain: {0}", Thread.GetDomain().FriendlyName); 
            Console.WriteLine("ID of current Context: {0}", Thread.CurrentContext.ContextID); 

            Console.WriteLine("Thread Name: {0}", primaryThread.Name); 
            Console.WriteLine("Has thread started?: {0}", primaryThread.IsAlive);
            Console.WriteLine("Priority Level: {0}", primaryThread.Priority);
            Console.WriteLine("Thread State: {0}", primaryThread.ThreadState);
        }

        public void ThreadsWithDelegates()
        {
            BinaryOp delegateAsync = new BinaryOp((a, b) =>
            {
                Console.WriteLine("Enter async delegate");

                Thread.Sleep(5000);

                Console.WriteLine("Exit async delegate");

                return a | b;
            });

            var iAsyncRes = delegateAsync.BeginInvoke(2, 5, null, null); //call delegate async in thread pool

            Console.WriteLine("Not async delegate");

            Console.WriteLine($"You can check IsCompleted at any time. IsCompleted = {iAsyncRes.IsCompleted}");

            int returnValue = delegateAsync.EndInvoke(iAsyncRes); //will wait until return from async

            Console.WriteLine($"Call result = {returnValue}");
        }

        public void ThreadsWithDelegatesAndCallback()
        {
            BinaryOp delegateAsync = new BinaryOp((a, b) =>
            {
                Console.WriteLine("Enter async delegate");

                Thread.Sleep(5000);

                Console.WriteLine("Exit async delegate");

                return a | b;
            });

            var iAsyncRes = delegateAsync.BeginInvoke(2, 5, CallbackMethod, "Some info to share"); //call delegate async in thread pool

            Console.WriteLine("Not async delegate");

            iAsyncRes.AsyncWaitHandle.WaitOne(); //block main Thread. Wait delegate here
        }

        // The callback method must have the same signature as the
        // AsyncCallback delegate. 
        // Will be called after BeginInvoke ending async in secondary thread!
        private static void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            BinaryOp caller = result.AsyncDelegate as BinaryOp;

            // Retrieve the string that was passed as state 
            // information.
            string someMessage = (string)ar.AsyncState;

            Console.WriteLine(someMessage);

            // Define a variable to receive the value of the out parameter.
            // If the parameter were ref rather than out then it would have to
            // be a class-level field so it could also be passed to BeginInvoke.
            int threadId = 0;

            // Call EndInvoke to retrieve the results.
            int returnValue = caller.EndInvoke( ar);

            // Use the format string to format the output message.
            Console.WriteLine($"Call result = {returnValue}");
        }
    
        public void NewThread()
        {
            var newThread = new Thread(() => //you can use Parametrized ThreadStart delegate here if you want
            {
                Console.WriteLine("In new thread");
                Thread.Sleep(5000);
                Console.WriteLine("In new thread - end");
            }); //will not start here. You should run it manualy 

            newThread.Priority = ThreadPriority.Lowest; // adjust priority if you need
            newThread.IsBackground = true; //make it background
            newThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR"); //adjust clock and numbering culture

            newThread.Start(); // run it

            newThread.Suspend(); //bad idea, but you can, obsolete

            Thread.Sleep(500);

            Console.WriteLine(newThread.ThreadState); //check thread state

            newThread.Resume(); // contunue - bad idea, obsolete
        }

        AutoResetEvent _resetEvent = new AutoResetEvent(false);

        public void UseAutoResetEvent()
        {
            var t = new Thread(LongTermIntAdd); //params but void

            t.Start();

            _resetEvent.WaitOne(6000); //timeline to wait, optional

            int a = 0;
        }

        private void LongTermIntAdd(object args)
        {
            var intArgs = args as int[];

            if (intArgs == null)
                return;

            Thread.Sleep(5000);

            Console.WriteLine($"Sum = {intArgs.Sum()}");

            _resetEvent.Set(); 
        }
    }
}
