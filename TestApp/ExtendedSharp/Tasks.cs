using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class Tasks
    {
        public void RunSimpleTask()
        {
            //Start new task in ThreadPool
            var t = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);

                Console.WriteLine($"Work in thread = {Thread.CurrentThread.ManagedThreadId}");
            });
        }

        public CancellationTokenSource RunWithCancelation()
        {
            var cancelationTocken = new CancellationTokenSource();

            var t = Task.Factory.StartNew(() =>
            {

                while (!cancelationTocken.IsCancellationRequested)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"Waiting for cancel");
                }


            }, cancelationTocken.Token);

            return cancelationTocken;
        }

        public async void DoAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");
            });

            Console.WriteLine("After await");
        }

        public async Task DoAsyncWithReturn()
        {
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");
            });

            Console.WriteLine("After await");
        }

        public Task<int> DoAsyncWithIntReturn()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");

                return 15;
            });
        }
    }

    public class TasksTester
    {
        public void TestAsync()
        {
            var t = new Tasks();
            t.DoAsync();

            Console.WriteLine("In TestAsync");
        }

        public void TestAsync2()
        {
            var t = new Tasks();
            var retTask = t.DoAsyncWithReturn();

            Console.WriteLine("In TestAsync");

            retTask.Wait();
        }

        public async void TestAsync3()
        {
            var t = new Tasks();
            var resultValue = await t.DoAsyncWithIntReturn();

            Console.WriteLine($"In TestAsync result = {resultValue}");
        }
    }
}
