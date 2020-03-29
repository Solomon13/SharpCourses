using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithEntityFramework
{
    public class EntityTester
    {
        /// <summary>
        /// Simple method to check how work with Entity
        /// </summary>
        public void CheckProductsWithEntityContext()
        {
            var context = new MySqlContext();

            foreach(var product in context.Products)
            {
                Console.WriteLine($"Product id = {product.ProductId}\tProduct name={product.ProductName}\tPrice = {product.Price}");
            }
        }

        public void WorkWithQueries()
        {
            //Open context
            var context = new MySqlContext();

            //Prepare query
            IQueryable<Product> productsQuery = context.Products;

            //execute query
            IEnumerable<Product> products = productsQuery.ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"Product id = {product.ProductId}\tProduct name={product.ProductName}\tPrice = {product.Price}");
            }

            //prepare filtered query
            //ere we build expression tree that will be used by IQueryProvider to transform it next to SQL query
            IQueryable<Product> filteredQuery = productsQuery.Where(p => p.ProductId == 2);
            //Execute filtered query
            IEnumerable<Product> filteredProducts = filteredQuery.ToList();

            foreach (var product in products)
            {
                Console.WriteLine($"Product id = {product.ProductId}\tProduct name={product.ProductName}\tPrice = {product.Price}");
            }

            context.Dispose();
        }

        public void WorkWithIEnumerableQuery()
        {
            //Open context
            var context = new MySqlContext();

            //cast to IEnumerable instead of IQueryable
            IEnumerable<Product> products = context.Products as IEnumerable<Product>;

            //Slow performance here, because Select * From Products will be used first and then filtered by IEnumerable
            IEnumerable<Product> filteredQueryToEnumerable = products.Where(p => p.ProductId == 2);
            var filteredQueryToEnumerableList = filteredQueryToEnumerable.ToList();

            context.Dispose();
        }

        public async void WorkWithQueriesOldFormat()
        {
            //Open context
            var context = new MySqlContext();

            //You still can run SQL dirrectly 
            var query = context.Database.SqlQuery(typeof(Product), "Select * From Products");

            var result = await query.ToListAsync();

            //You can cast them
            foreach (Product product in result)
            {
                Console.WriteLine($"Product id = {product.ProductId}\tProduct name={product.ProductName}\tPrice = {product.Price}");
            }

            context.Dispose();
        }

        public void WorkWithSqlBaseSyntax()
        {
            using(var context = new MySqlContext())
            {
                //prepare query using SQL based syntax
                var filteredCustomersQuery = from p in context.Products
                                             where p.Price > 2
                                             select p;

                //execute
                var products = filteredCustomersQuery.ToList();
            }
        }

        public void WorkWithComplexRequest()
        {
            using (var context = new MySqlContext())
            {
                //prepare query using SQL based syntax with joins
                var filteredJoinQuery = from c in context.Customers
                                             join o in context.Orders on c.CustomerID equals o.CustomerID
                                             join op in context.OrderProducts on o.OrderID equals op.OrderID
                                             select new
                                             {
                                                 CustomerName = c.CustomerName,
                                                 OrderId = o.OrderID,
                                                 OrderDate = o.OrderDate,
                                                 Count = op.Count
                                             };

                //execute
                var results = filteredJoinQuery.ToList();
            }
        }

        public void TestNavigationLinks()
        {
            using (var context = new MySqlContext())
            {
                try
                {
                    foreach (var customer in context.Customers)
                    {
                        foreach (var order in customer.Orders)
                        {
                            Console.WriteLine($"Customer = {customer.CustomerName}\tOrder={order.OrderID}");
                        }
                    }
                }
                catch(EntityCommandExecutionException e)
                {
                    Console.WriteLine("Oups. Problem of creation queries. See sample below how to fix it");
                }



                var customers = context.Customers.ToList();

                //It's better now. No crash. But performance.... Pay attention to debug output to see what queries generated for DB
                foreach (var customer in customers)
                {
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"Customer = {customer.CustomerName}\tOrder={order.OrderID}");
                    }
                }


                //Use Include method
                customers = context.Customers.Include("Orders").ToList();

                //Include("Orders.OrderProducts") -> use this include sub items

                foreach (var customer in customers)
                {
                    foreach (var order in customer.Orders)
                    {
                        Console.WriteLine($"Customer = {customer.CustomerName}\tOrder={order.OrderID}");
                    }
                }

            }
        }
    }
}
