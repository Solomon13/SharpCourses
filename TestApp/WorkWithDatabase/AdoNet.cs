﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkWithDatabase
{
    /// <summary>
    /// This this my Ado sample class
    /// </summary>
    public class AdoNet
    {
        /// <summary>
        /// Created new <see cref="IDbConnection"/> instance for MySql database
        /// </summary>
        /// <returns>New db instance. Ensure to close it when not used</returns>
        public IDbConnection CreateConnection()
        {
            IDbConnection conn = null;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = Properties.Settings.Default.DbConnection;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                conn = null;
                Console.WriteLine(ex.Message);
            }

            return conn;
        }

        /// <summary>
        /// Creates connection async
        /// </summary>
        /// <returns>Opened connection</returns>
        public Task<IDbConnection> CreateConnectionAsync()
        {
            return Task.Factory.StartNew(() => CreateConnection());
        }

        public class Product
        {
            public int Id { get; set; }
            public string ProductName { get; set; }
            public double Price { get; set; }
        }

        /// <summary>
        /// Executes specified query
        /// </summary>
        /// <param name="query"></param>
        public IEnumerable<Product> GetProducts(IDbConnection conn, bool bPrint = true)
        {
            List<Product> products = new List<Product>();

            //Open Database

            string query = "Select * from Products";

            //Prepapare command to execure sql
            IDbCommand command = new MySql.Data.MySqlClient.MySqlCommand(query);
            command.Connection = conn;

            //Prepare reader to receive data
            IDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                try
                {
                    //Be careful with types convertion 
                    var product = new Product
                    {
                        Id = reader.GetInt32(0),
                        ProductName = reader["ProductName"].ToString(),
                        Price = reader.GetDouble(2)
                    };

                    if (bPrint)
                        Console.WriteLine($"ProductId = {product.Id}\tProduct Name = {product.ProductName}\tPrice = {product.Price}");

                    products.Add(product);
                }
                catch
                {
                    Console.WriteLine("Failed to read data");
                }
            }

            return products;
        }

        public Task<IEnumerable<Product>> GetProductsAsync(IDbConnection conn, int delay = 2000)
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(delay);
                return GetProducts(conn, false);
            });
        }
    }
}
