using ExtendedSharp;
using OopSharp;
using ReferenceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkWithDatabase;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = TestDbAsync();

            while(!t.IsCompleted)
            {
                Console.WriteLine("Main tread still alive");
                Thread.Sleep(50);
            }

            Console.ReadLine();
        }

        public static async Task TestDbAsync()
        {
            var db = new AdoNet();
            var fileName = await db.WriteCustomersToFilesAsync();

            Console.WriteLine($"Writing to file completed. File name = {fileName}");
        }
    }
}
