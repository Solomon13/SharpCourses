namespace CodeFirst
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class TestDbContext : DbContext
    {
        // Your context has been configured to use a 'TestDataModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'CodeFirst.TestDataModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TestDataModel' 
        // connection string in the application configuration file.
        public TestDbContext()
            : base("name=TestDataModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Car> Cars { get; set; }
        //public virtual DbSet<Customer> Customers { get; set; }
    }

    public class Car
    {
        [Key]
        [MaxLength(250)]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
       
        public double Engine { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }

    }
}