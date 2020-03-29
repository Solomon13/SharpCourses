using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithEntityFramework
{
    public class EntityUpdateTester
    {
        public void UpdateRecord()
        {
            using (var context = new MySqlContext())
            {
                var customer = context.Customers.Single(c => c.CustomerID == 2);

                customer.CustomerName = string.Compare(customer.CustomerName, "Jacky Chan", true) == 0 ? "Brad Pit" : "Jacky Chan";

                //use this method to update DB. Or use SaveChangesAsync 
                //Pay attention, DB sync will work only if there were modifications
                context.SaveChanges();
            }
        }

        public void UpdateRecordsInDifferentContext()
        {
            var context1 = new MySqlContext();
            var context2 = new MySqlContext();

            var customer1 = context1.Customers.Single(c => c.CustomerID == 2);
            var customer2 = context2.Customers.Single(c => c.CustomerID == 2);

            customer1.CustomerName = string.Compare(customer1.CustomerName, "Jacky Chan", true) == 0 ?  "Brad Pit" : "Jacky Chan";

            context1.SaveChanges();

            Console.WriteLine(customer2.CustomerName);

            context1.Dispose();
            context2.Dispose();
        }

        public void AddNewEntries()
        {
            using (var context = new MySqlContext())
            {
                var newCustomer = context.Customers.Add(new Customer
                {
                    CustomerName = "Paul McArtur"
                });

                var order = context.Orders.Add(new Order
                {
                    CustomerID = newCustomer.CustomerID,
                    OrderDate = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}
