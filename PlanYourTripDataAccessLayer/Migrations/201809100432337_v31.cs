namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v31 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payment", "CreditCardNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payment", "CreditCardNumber", c => c.Int(nullable: false));
        }
    }
}
