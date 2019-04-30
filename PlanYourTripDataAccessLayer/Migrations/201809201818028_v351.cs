namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v351 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PackageBooking", "PackageID", "dbo.Package");
        }
        
        public override void Down()
        {
        }
    }
}
