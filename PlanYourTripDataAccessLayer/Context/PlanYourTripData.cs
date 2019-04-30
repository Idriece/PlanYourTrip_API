using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanYourTripBusinessEntity.Models;

namespace PlanYourTripDataAccessLayer.Context
{
    public class PlanYourTripData:DbContext
    {
        public PlanYourTripData() : base("name = PlanYourTrip")
        {

        }

        
        
        public DbSet<WishList> WishLists { get; set; }
        
        public DbSet<UserInterest> UserInterests { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RoomPrice> RoomPrices { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<TransportationProvider> TransportationProviders { get; set; }
        public DbSet<TransportationMode> TransportationModes { get; set; }
        public DbSet<TransportationPrice> TransportationPrices { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<CustomPackage> CustomPackages { get; set; }
        public DbSet<UserCustomization> UserCustomizations { get; set; }
        public DbSet<PackageBooking> PackageBookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<UserCheckIn> UserCheckIns { get; set; }
        public DbSet<Refunds> Refunds { get; set; }
        public DbSet<RefundsRules> RefundsRules { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<IdentityUserLogin> UserLogin { get; set;}
        public DbSet<IdentityUserRole> UserRole { get; set; }
        public DbSet<IdentityRole> Role { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //AspNetUsers -> User
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("User");
            //AspNetRoles -> Role
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Role");
            //AspNetUserRoles -> UserRole
            modelBuilder.Entity<IdentityUserRole>()
                .ToTable("UserRole");
            //AspNetUserClaims -> UserClaim
            modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("UserClaim");
            //AspNetUserLogins -> UserLogin
            modelBuilder.Entity<IdentityUserLogin>()
                .ToTable("UserLogin");
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
