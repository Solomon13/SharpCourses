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

            var t = Task.Factory.StartNew(s =>
            {
                Console.WriteLine(s.GetType().ToString());

                var tocken = (CancellationToken)s;

                while (true/*!cancelationTocken.IsCancellationRequested*/)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        Console.WriteLine($"Waiting for cancel");
                        tocken.ThrowIfCancellationRequested();
                    }
                    catch(OperationCanceledException)
                    {
                        break;
                    }
                }


            }, cancelationTocken.Token);

            return cancelationTocken;
        }

        public async Task DoAsync()
        {
            Console.WriteLine("Sync");

            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");
            });

            Console.WriteLine("After await");

            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(10000); //Long tearm actions

                Console.WriteLine("In task");
            });

            Console.WriteLine("After await");
        }

        public void Do2Async()
        {
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");

                return 15;
            }).
            ContinueWith(t =>
            {
                Console.WriteLine($"After await {t.AsyncState}");
            });
        }

        public async Task DoWithReturnAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");
            });

            Console.WriteLine("After await");
        }

        public Task<int> DoWithIntReturnAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");

                return 15;
            });
        }

        public async Task DoWithExceptionAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(5000); //Long tearm actions

                Console.WriteLine("In task");

                throw new Exception("EXCEPTION!!!!");
            });

            Console.WriteLine("After exception");
        }

        //public async string NotPossible()
        //{

        //}

    }

    public class TasksTester
    {
        public void TaskCancelTester()
        {
            var tester = new Tasks();
            
            var cs = tester.RunWithCancelation();

            //Thread.Sleep(10000);

            cs.CancelAfter(10000);
        }

        public void TestAsync()
        {
            Console.WriteLine("Before Async/await");
            
            var t = new Tasks();
            t.DoAsync();

            Console.WriteLine("After async/await");
        }

        public void TestAsync2()
        {
            var t = new Tasks();
            var retTask = t.DoWithReturnAsync();

            Console.WriteLine("In TestAsync");

            retTask.Wait();
        }

        public async Task TestAsync3()
        {
            var t = new Tasks();
           
            await t.DoAsync();

            int resultValue = await t.DoWithIntReturnAsync();

            Console.WriteLine($"In TestAsync result = {resultValue}");
        }

        public void TestAsyncWithException()
        {
            var t = new Tasks();

            try
            {
                t.DoWithExceptionAsync().Wait();
            }
            catch
            {
                Console.WriteLine("Catched!!!");
            }
        }

        public async void TestLampdaAsync()
        {
            //you can use async for lampda
            var res = await Task.Run(async() =>
            {
                var t = new Tasks();
                return await t.DoWithIntReturnAsync();
            });
        }

        public void UsefulTaskMethods()
        {
            var t = Task.Run(() => { Thread.Sleep(10000); });

            Task.WaitAll(new[] { t });

            var t1 = Task.Run(() => { Thread.Sleep(10000); });

            t.ContinueWith(prevTask =>
            {
                Thread.Sleep(10000);
            });
        }
    }
}
