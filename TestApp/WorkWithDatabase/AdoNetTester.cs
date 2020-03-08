﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkWithDatabase
{
    public class AdoNetTester
    {
        public async Task TaskTestDbWithAsync()
        {
            var db = new AdoNet();
            var products = await db.GetProductsAsync();

            var fileName = "test.txt";

            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (var product in products)
                {
                    await file.WriteLineAsync($"ProductId = {product.Id}\tProduct Name = {product.ProductName}\tPrice = {product.Price}");
                }
            }

            Console.WriteLine($"Writing to file completed. File name = {fileName}");
        }

        public Thread TestDbWithThread()
        {
            var workThread = new Thread(() =>
            {
                var db = new AdoNet();
                var products = db.GetProducts(false);
                Thread.Sleep(2000);

                var fileName = "test.txt";

                using (StreamWriter file = new StreamWriter(fileName))
                {
                    foreach (var product in products)
                    {
                        file.WriteLine($"ProductId = {product.Id}\tProduct Name = {product.ProductName}\tPrice = {product.Price}");
                    }
                }

                Console.WriteLine($"Writing to file completed. File name = {fileName}");
            });

            workThread.Start();

            return workThread;
        }
    }
}
