namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCheckIn", "HotelID", c => c.Int(nullable: false));
            AddColumn("dbo.UserCheckIn", "TransportationProviderID", c => c.Int(nullable: false));
            AddColumn("dbo.UserCheckIn", "CheckInDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.UserCheckIn", "TransportationCheckINStatus", c => c.Boolean(nullable: false));
            CreateIndex("dbo.UserCheckIn", "HotelID");
            CreateIndex("dbo.UserCheckIn", "TransportationProviderID");
            AddForeignKey("dbo.UserCheckIn", "HotelID", "dbo.Hotel", "HotelID");
            AddForeignKey("dbo.UserCheckIn", "TransportationProviderID", "dbo.TransportationProvider", "TransportationProviderID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCheckIn", "TransportationProviderID", "dbo.TransportationProvider");
            DropForeignKey("dbo.UserCheckIn", "HotelID", "dbo.Hotel");
            DropIndex("dbo.UserCheckIn", new[] { "TransportationProviderID" });
            DropIndex("dbo.UserCheckIn", new[] { "HotelID" });
            DropColumn("dbo.UserCheckIn", "TransportationCheckINStatus");
            DropColumn("dbo.UserCheckIn", "CheckInDate");
            DropColumn("dbo.UserCheckIn", "TransportationProviderID");
            DropColumn("dbo.UserCheckIn", "HotelID");
        }
    }
}
