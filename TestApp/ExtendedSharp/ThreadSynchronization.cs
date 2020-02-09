using System;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace ExtendedSharp
{
    public enum enumSynchStateTester
    {
        NoLock,
        Lock,
        Monitor,
        Synchronization
    }

    public static class Printer
    {
        private static object SynchObject = new object();

        private static readonly int SleepTime = 100;

        public static void PrintNumbers(object waitObj)
        {
            Console.WriteLine($"Printing from Thread = {Thread.CurrentThread.Name}");

            for (int i = 0; i < 10; i++)
            {

                Random r = new Random();
                Thread.Sleep(SleepTime * r.Next(5));
                Console.Write("{0}, ", i);
            }

            Console.WriteLine();

            var wait = waitObj as AutoResetEvent;
            wait?.Set();
        }

        public static void PrintNumbersWithLock(object waitObj)
        {
            lock (SynchObject)
            {
                Console.WriteLine($"Printing from Thread = {Thread.CurrentThread.Name}");

                for (int i = 0; i < 10; i++)
                {

                    Random r = new Random();
                    Thread.Sleep(SleepTime * r.Next(5));
                    Console.Write("{0}, ", i);
                }

                Console.WriteLine();

                var wait = waitObj as AutoResetEvent;
                wait?.Set();
            }
        }

        public static void PrintNumbersWithMonitor(object waitObj)
        {
            Monitor.Enter(SynchObject);

            try
            {
                Console.WriteLine($"Printing from Thread = {Thread.CurrentThread.Name}");

                for (int i = 0; i < 10; i++)
                {

                    Random r = new Random();
                    Thread.Sleep(SleepTime * r.Next(5));
                    Console.Write("{0}, ", i);
                }

                Console.WriteLine();

                var wait = waitObj as AutoResetEvent;
                wait?.Set();
            }
            finally
            {
                Monitor.Exit(SynchObject);
            }
        }
    }

    [Synchronization] //all methods inside this class now thread safe
    public class SynchPrinter : ContextBoundObject
    {
        public void PrintNumbers(object waitObj)
        {
            Console.WriteLine($"Printing from Thread = {Thread.CurrentThread.Name}");

            for (int i = 0; i < 10; i++)
            {

                Random r = new Random();
                Thread.Sleep(1000 * r.Next(5));
                Console.Write("{0}, ", i);
            }

            Console.WriteLine();

            var wait = waitObj as AutoResetEvent;
            wait?.Set();
        }
    }

    public static class ThreadStartInfoBuilder
    {
        public static ParameterizedThreadStart Build(enumSynchStateTester type)
        {
            switch(type)
            {
                case enumSynchStateTester.NoLock:
                    return new ParameterizedThreadStart(Printer.PrintNumbers);
                case enumSynchStateTester.Lock:
                    return new ParameterizedThreadStart(Printer.PrintNumbersWithLock);
                case enumSynchStateTester.Monitor:
                    return new ParameterizedThreadStart(Printer.PrintNumbersWithMonitor);
                case enumSynchStateTester.Synchronization:
                    var p = new SynchPrinter();
                    return new ParameterizedThreadStart(p.PrintNumbers);
            }

            throw new ArgumentException("Can't build with provided args");
        }
    }

    public class ThreadSynchronization
    {
        public void ThreadsHellTest(enumSynchStateTester type)
        {
            Thread[] threads = new Thread[10];
            AutoResetEvent[] waiters = new AutoResetEvent[10];

            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(ThreadStartInfoBuilder.Build(type))
                {
                    Name = $"Worker thread #{i}",
                    Priority = ThreadPriority.Highest
                };

                waiters[i] = new AutoResetEvent(false);
            }

            for (int i = 0; i < 10; i++)
            {
                threads[i].Start(waiters[i]);
            }

            WaitHandle.WaitAll(waiters);
        }
    }
}
