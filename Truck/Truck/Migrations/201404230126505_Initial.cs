namespace Truck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodTrucks", "Map", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FoodTrucks", "Map");
        }
    }
}
