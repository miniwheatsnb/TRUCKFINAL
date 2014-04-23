namespace Truck.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodTrucks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TruckName = c.String(),
                        Location = c.String(),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FoodTrucks");
        }
    }
}
