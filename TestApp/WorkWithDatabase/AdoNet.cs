using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkWithDatabase
{

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
        public IEnumerable<Product> GetProducts(bool bPrint = true)
        {
            List<Product> products = new List<Product>();

            //Open Database
            using(var conn = CreateConnection())
            {
                string query = "Select * from Products";

                //Prepapare command to execure sql
                IDbCommand command = new MySql.Data.MySqlClient.MySqlCommand(query);
                command.Connection = conn;

                //Prepare reader to receive data
                IDataReader reader = command.ExecuteReader();

                while(reader.Read())
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
          
                        if(bPrint)
                            Console.WriteLine($"ProductId = {product.Id}\tProduct Name = {product.ProductName}\tPrice = {product.Price}");

                        products.Add(product);
                    }
                    catch
                    {
                        Console.WriteLine("Failed to read data");
                    }
                }
            }

            return products;
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return GetProducts(false);
            });
        }

        public async Task<string> WriteCustomersToFilesAsync()
        {
            var products = await GetProductsAsync();
            var fileName = "test.txt";

            using (StreamWriter file = new StreamWriter(fileName))
            {
                foreach (var product in products)
                {
                    await file.WriteLineAsync($"ProductId = {product.Id}\tProduct Name = {product.ProductName}\tPrice = {product.Price}");
                }
            }

            return fileName;
        }
    }
}
