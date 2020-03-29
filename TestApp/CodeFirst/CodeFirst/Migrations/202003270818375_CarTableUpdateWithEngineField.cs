namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarTableUpdateWithEngineField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cars", "Engine", c => c.Double(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cars", "Engine");
        }
    }
}
