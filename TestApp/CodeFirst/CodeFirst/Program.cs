using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new TestDbContext())
            {
                context.Cars.Add(new Car
                {
                    Id = 1,
                    Name = "Opel",
                    Engine = 1.0
                });

                context.SaveChanges();
            }
        }
    }
}
