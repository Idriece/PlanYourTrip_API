namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Refunds",
                c => new
                    {
                        RefundsId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PackageBookingID = c.Int(nullable: false),
                        RefundRuleId = c.Int(nullable: false),
                        RefundDate = c.DateTime(nullable: false),
                        RefundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RefundsId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.PackageBooking", t => t.PackageBookingID)
                .ForeignKey("dbo.RefundsRules", t => t.RefundRuleId)
                .Index(t => t.UserId)
                .Index(t => t.PackageBookingID)
                .Index(t => t.RefundRuleId);
            
            CreateTable(
                "dbo.RefundsRules",
                c => new
                    {
                        RefundRuleId = c.Int(nullable: false, identity: true),
                        Days = c.Int(nullable: false),
                        RefundPercentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RefundRuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Refunds", "RefundRuleId", "dbo.RefundsRules");
            DropForeignKey("dbo.Refunds", "PackageBookingID", "dbo.PackageBooking");
            DropForeignKey("dbo.Refunds", "UserId", "dbo.User");
            DropIndex("dbo.Refunds", new[] { "RefundRuleId" });
            DropIndex("dbo.Refunds", new[] { "PackageBookingID" });
            DropIndex("dbo.Refunds", new[] { "UserId" });
            DropTable("dbo.RefundsRules");
            DropTable("dbo.Refunds");
        }
    }
}
