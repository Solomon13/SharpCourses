using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithEntityFramework
{
    public class EntityTester
    {
        public void CheckProductsWithEntityContext()
        {
            var context = new MySqlContext();

            foreach(var product in context.Products)
            {
                Console.WriteLine($"Product id = {product.ProductId}\tProduct name={product.ProductName}\tPrice = {product.Price}");
            }
        }
    }
}
