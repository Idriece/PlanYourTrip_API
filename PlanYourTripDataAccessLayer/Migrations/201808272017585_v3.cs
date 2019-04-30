namespace PlanYourTripDataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        StateID = c.Int(nullable: false),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityID)
                .ForeignKey("dbo.State", t => t.StateID)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        StateID = c.Int(nullable: false, identity: true),
                        StateName = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.StateID);
            
            CreateTable(
                "dbo.CustomPackage",
                c => new
                    {
                        CustomPackageID = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CustomPackageID)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(maxLength: 50, unicode: false),
                        CityID = c.Int(nullable: false),
                        NumberOfTrips = c.Int(nullable: false),
                        SecurityQuestion1 = c.String(maxLength: 1000),
                        SecurityAnswer1 = c.String(maxLength: 200),
                        SecurityQuestion2 = c.String(maxLength: 1000),
                        SecurityAnswer2 = c.String(maxLength: 200),
                        SecurityQuestion3 = c.String(maxLength: 1000),
                        SecurityAnswer3 = c.String(maxLength: 200),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Role", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.FeedBack",
                c => new
                    {
                        FeedBackID = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        PackageID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                        Review = c.String(maxLength: 2000),
                    })
                .PrimaryKey(t => t.FeedBackID)
                .ForeignKey("dbo.User", t => t.Id)
                .ForeignKey("dbo.Package", t => t.PackageID)
                .Index(t => t.Id)
                .Index(t => t.PackageID);
            
            CreateTable(
                "dbo.Package",
                c => new
                    {
                        PackageID = c.Int(nullable: false, identity: true),
                        PackageTypeID = c.Int(nullable: false),
                        PackageName = c.String(maxLength: 100, unicode: false),
                        Days = c.Int(nullable: false),
                        ProfitPercentage = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Summary = c.String(maxLength: 1000, unicode: false),
                        NumberAvailable = c.Int(nullable: false),
                        MinPeople = c.Int(nullable: false),
                        MaxPeople = c.Int(nullable: false),
                        Image = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.PackageID)
                .ForeignKey("dbo.PackageType", t => t.PackageTypeID)
                .Index(t => t.PackageTypeID);
            
            CreateTable(
                "dbo.PackageType",
                c => new
                    {
                        PackageTypeID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 8000, unicode: false),
                    })
                .PrimaryKey(t => t.PackageTypeID);
            
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        HotelID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        HotelName = c.String(maxLength: 100, unicode: false),
                        Id = c.String(maxLength: 128),
                        HotelImage = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.HotelID)
                .ForeignKey("dbo.User", t => t.Id)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Itinerary",
                c => new
                    {
                        ItineraryID = c.Int(nullable: false, identity: true),
                        PackageID = c.Int(nullable: false),
                        RoomPriceID = c.Int(nullable: false),
                        TransportationPriceID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        ActivityDetails = c.String(maxLength: 450),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItineraryID)
                .ForeignKey("dbo.Package", t => t.PackageID)
                .ForeignKey("dbo.RoomPrice", t => t.RoomPriceID)
                .ForeignKey("dbo.TransportationPrice", t => t.TransportationPriceID)
                .Index(t => t.PackageID)
                .Index(t => t.RoomPriceID)
                .Index(t => t.TransportationPriceID);
            
            CreateTable(
                "dbo.RoomPrice",
                c => new
                    {
                        RoomPriceID = c.Int(nullable: false, identity: true),
                        RoomTypeID = c.Int(nullable: false),
                        HotelID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RoomPriceID)
                .ForeignKey("dbo.Hotel", t => t.HotelID)
                .ForeignKey("dbo.RoomType", t => t.RoomTypeID)
                .Index(t => t.RoomTypeID)
                .Index(t => t.HotelID);
            
            CreateTable(
                "dbo.RoomType",
                c => new
                    {
                        RoomTypeID = c.Int(nullable: false, identity: true),
                        Type = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.RoomTypeID);
            
            CreateTable(
                "dbo.TransportationPrice",
                c => new
                    {
                        TransportationPriceID = c.Int(nullable: false, identity: true),
                        TransportationProviderID = c.Int(nullable: false),
                        TransportationModeID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransportationPriceID)
                .ForeignKey("dbo.TransportationMode", t => t.TransportationModeID)
                .ForeignKey("dbo.TransportationProvider", t => t.TransportationProviderID)
                .Index(t => t.TransportationProviderID)
                .Index(t => t.TransportationModeID);
            
            CreateTable(
                "dbo.TransportationMode",
                c => new
                    {
                        TransportationModeID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        NumberOfSeats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransportationModeID);
            
            CreateTable(
                "dbo.TransportationProvider",
                c => new
                    {
                        TransportationProviderID = c.Int(nullable: false, identity: true),
                        CityID = c.Int(nullable: false),
                        TransportationProviderName = c.String(maxLength: 100, unicode: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransportationProviderID)
                .ForeignKey("dbo.User", t => t.Id)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PackageBooking",
                c => new
                    {
                        PackageBookingID = c.Int(nullable: false, identity: true),
                        PackageID = c.Int(nullable: false),
                        Id = c.String(maxLength: 128),
                        NumPeople = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        PaymentMethod = c.String(maxLength: 50, unicode: false),
                        IsCustomized = c.Boolean(nullable: false),
                        TotalCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingStatus = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.PackageBookingID)
                .ForeignKey("dbo.User", t => t.Id)
                .ForeignKey("dbo.Package", t => t.PackageID)
                .Index(t => t.PackageID)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Int(nullable: false, identity: true),
                        PackageBookingID = c.Int(nullable: false),
                        CreditCardNumber = c.Int(nullable: false),
                        NameOnCard = c.String(maxLength: 100, unicode: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.PackageBooking", t => t.PackageBookingID)
                .Index(t => t.PackageBookingID);
            
            CreateTable(
                "dbo.UserCheckIn",
                c => new
                    {
                        CheckInID = c.Int(nullable: false, identity: true),
                        PackageBookingID = c.Int(nullable: false),
                        HotelCheckINStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CheckInID)
                .ForeignKey("dbo.PackageBooking", t => t.PackageBookingID)
                .Index(t => t.PackageBookingID);
            
            CreateTable(
                "dbo.User Customization",
                c => new
                    {
                        CustomizationID = c.Int(nullable: false, identity: true),
                        CustomPackageID = c.Int(nullable: false),
                        PackageID = c.Int(nullable: false),
                        RoomPriceID = c.Int(nullable: false),
                        TransportationPriceID = c.Int(nullable: false),
                        DayNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomizationID)
                .ForeignKey("dbo.CustomPackage", t => t.CustomPackageID)
                .ForeignKey("dbo.Package", t => t.PackageID)
                .ForeignKey("dbo.RoomPrice", t => t.RoomPriceID)
                .ForeignKey("dbo.TransportationPrice", t => t.TransportationPriceID)
                .Index(t => t.CustomPackageID)
                .Index(t => t.PackageID)
                .Index(t => t.RoomPriceID)
                .Index(t => t.TransportationPriceID);
            
            CreateTable(
                "dbo.UserInterest",
                c => new
                    {
                        UserInterestID = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        Interest = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.UserInterestID)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.WishList",
                c => new
                    {
                        WishListID = c.Int(nullable: false, identity: true),
                        Id = c.String(maxLength: 128),
                        PackageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishListID)
                .ForeignKey("dbo.User", t => t.Id)
                .ForeignKey("dbo.Package", t => t.PackageID)
                .Index(t => t.Id)
                .Index(t => t.PackageID);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "IdentityRole_Id", "dbo.Role");
            DropForeignKey("dbo.WishList", "PackageID", "dbo.Package");
            DropForeignKey("dbo.WishList", "Id", "dbo.User");
            DropForeignKey("dbo.UserInterest", "Id", "dbo.User");
            DropForeignKey("dbo.User Customization", "TransportationPriceID", "dbo.TransportationPrice");
            DropForeignKey("dbo.User Customization", "RoomPriceID", "dbo.RoomPrice");
            DropForeignKey("dbo.User Customization", "PackageID", "dbo.Package");
            DropForeignKey("dbo.User Customization", "CustomPackageID", "dbo.CustomPackage");
            DropForeignKey("dbo.UserCheckIn", "PackageBookingID", "dbo.PackageBooking");
            DropForeignKey("dbo.Payment", "PackageBookingID", "dbo.PackageBooking");
            DropForeignKey("dbo.PackageBooking", "PackageID", "dbo.Package");
            DropForeignKey("dbo.PackageBooking", "Id", "dbo.User");
            DropForeignKey("dbo.Itinerary", "TransportationPriceID", "dbo.TransportationPrice");
            DropForeignKey("dbo.TransportationPrice", "TransportationProviderID", "dbo.TransportationProvider");
            DropForeignKey("dbo.TransportationProvider", "CityID", "dbo.City");
            DropForeignKey("dbo.TransportationProvider", "Id", "dbo.User");
            DropForeignKey("dbo.TransportationPrice", "TransportationModeID", "dbo.TransportationMode");
            DropForeignKey("dbo.Itinerary", "RoomPriceID", "dbo.RoomPrice");
            DropForeignKey("dbo.RoomPrice", "RoomTypeID", "dbo.RoomType");
            DropForeignKey("dbo.RoomPrice", "HotelID", "dbo.Hotel");
            DropForeignKey("dbo.Itinerary", "PackageID", "dbo.Package");
            DropForeignKey("dbo.Hotel", "CityID", "dbo.City");
            DropForeignKey("dbo.Hotel", "Id", "dbo.User");
            DropForeignKey("dbo.FeedBack", "PackageID", "dbo.Package");
            DropForeignKey("dbo.Package", "PackageTypeID", "dbo.PackageType");
            DropForeignKey("dbo.FeedBack", "Id", "dbo.User");
            DropForeignKey("dbo.CustomPackage", "Id", "dbo.User");
            DropForeignKey("dbo.UserRole", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.UserLogin", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.UserClaim", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.User", "CityID", "dbo.City");
            DropForeignKey("dbo.City", "StateID", "dbo.State");
            DropIndex("dbo.WishList", new[] { "PackageID" });
            DropIndex("dbo.WishList", new[] { "Id" });
            DropIndex("dbo.UserInterest", new[] { "Id" });
            DropIndex("dbo.User Customization", new[] { "TransportationPriceID" });
            DropIndex("dbo.User Customization", new[] { "RoomPriceID" });
            DropIndex("dbo.User Customization", new[] { "PackageID" });
            DropIndex("dbo.User Customization", new[] { "CustomPackageID" });
            DropIndex("dbo.UserCheckIn", new[] { "PackageBookingID" });
            DropIndex("dbo.Payment", new[] { "PackageBookingID" });
            DropIndex("dbo.PackageBooking", new[] { "Id" });
            DropIndex("dbo.PackageBooking", new[] { "PackageID" });
            DropIndex("dbo.TransportationProvider", new[] { "Id" });
            DropIndex("dbo.TransportationProvider", new[] { "CityID" });
            DropIndex("dbo.TransportationPrice", new[] { "TransportationModeID" });
            DropIndex("dbo.TransportationPrice", new[] { "TransportationProviderID" });
            DropIndex("dbo.RoomPrice", new[] { "HotelID" });
            DropIndex("dbo.RoomPrice", new[] { "RoomTypeID" });
            DropIndex("dbo.Itinerary", new[] { "TransportationPriceID" });
            DropIndex("dbo.Itinerary", new[] { "RoomPriceID" });
            DropIndex("dbo.Itinerary", new[] { "PackageID" });
            DropIndex("dbo.Hotel", new[] { "Id" });
            DropIndex("dbo.Hotel", new[] { "CityID" });
            DropIndex("dbo.Package", new[] { "PackageTypeID" });
            DropIndex("dbo.FeedBack", new[] { "PackageID" });
            DropIndex("dbo.FeedBack", new[] { "Id" });
            DropIndex("dbo.UserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.UserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.User", new[] { "CityID" });
            DropIndex("dbo.CustomPackage", new[] { "Id" });
            DropIndex("dbo.City", new[] { "StateID" });
            DropTable("dbo.Role");
            DropTable("dbo.WishList");
            DropTable("dbo.UserInterest");
            DropTable("dbo.User Customization");
            DropTable("dbo.UserCheckIn");
            DropTable("dbo.Payment");
            DropTable("dbo.PackageBooking");
            DropTable("dbo.TransportationProvider");
            DropTable("dbo.TransportationMode");
            DropTable("dbo.TransportationPrice");
            DropTable("dbo.RoomType");
            DropTable("dbo.RoomPrice");
            DropTable("dbo.Itinerary");
            DropTable("dbo.Hotel");
            DropTable("dbo.PackageType");
            DropTable("dbo.Package");
            DropTable("dbo.FeedBack");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.CustomPackage");
            DropTable("dbo.State");
            DropTable("dbo.City");
        }
    }
}
