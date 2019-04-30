namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v32 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Payment", "CreditCardNumber", c => c.String(maxLength: 20, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payment", "CreditCardNumber", c => c.Long(nullable: false));
        }
    }
}
