using System;
using System.Threading;
using System.Windows.Threading;

namespace ExtendedSharp
{
    public class ThreadingExtended
    {
        private Thread _thread;

        public ThreadingExtended()
        {
            var waiting = new AutoResetEvent(false);

            _thread = new Thread(() =>
            {
                //Force Dispatcher creation for current thread
                var disp = Dispatcher.CurrentDispatcher;
                waiting.Set();

                //Run thread Dispatcher here. Keep thread alive until Dispatcher shutdown
                Dispatcher.Run();
            });

            _thread.Start();
            waiting.WaitOne();
        }

        public Dispatcher GetDispatcher()
        {
            return Dispatcher.FromThread(_thread);
        }

        public bool StopThread()
        {
            var dispatcher = GetDispatcher();

            if (dispatcher != null)
            {
                var waiting = new AutoResetEvent(false);

                dispatcher.ShutdownFinished += (o, e) =>
                {
                    waiting.Set();
                };

                dispatcher.InvokeShutdown();

                waiting.WaitOne();

                while(!dispatcher.HasShutdownFinished)
                    Thread.Sleep(10);
            }

            return !_thread.IsAlive;
        }
    }

    public class ThreadingExtendedTester
    {
        public void TestDispatcher()
        {
            var threading = new ThreadingExtended();

            Console.WriteLine($"Original hread ID = {Thread.CurrentThread.ManagedThreadId}");

            //Run in another thread but in blocking way
            threading.GetDispatcher().Invoke(() =>
            {
                Thread.Sleep(1000);

                Console.WriteLine($"Hello from Dispatcher in thread = {Thread.CurrentThread.ManagedThreadId}");
            });








            //Run in another thread when IDLE but in blocking way
            threading.GetDispatcher().Invoke(() =>
            {
                Thread.Sleep(1000);

                Console.WriteLine($"Hello from Dispatcher in thread IDLE = {Thread.CurrentThread.ManagedThreadId} ");
            }, DispatcherPriority.ContextIdle);








            var waiting = new AutoResetEvent(false);

            //Run in another thread but in async way
            threading.GetDispatcher().BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                Thread.Sleep(1000);

                Console.WriteLine($"Hello from Dispatcher in thread = {Thread.CurrentThread.ManagedThreadId}");
                waiting.Set();
            }));







            Console.WriteLine("Hello from Main");

            waiting.WaitOne();

            Console.WriteLine($"Is closed thread = {threading.StopThread()}");
        }
    
        public void TestTreadPool()
        {
            ThreadPool.GetAvailableThreads(out int workThreads, out int ports);
            Console.WriteLine($"Available threads in pool = {workThreads}");

            ThreadPool.GetMaxThreads(out int maxThreads, out int maxPorts);
            Console.WriteLine($"Max threads in pool = {maxThreads}");

            ThreadPool.SetMinThreads(10, 10);

            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(s =>
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Work in thread = {Thread.CurrentThread.ManagedThreadId}");
                }));
            }
        }
    }
}
