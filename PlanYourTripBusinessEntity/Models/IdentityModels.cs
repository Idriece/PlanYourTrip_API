using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessEntity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50)]
        [Column(TypeName ="VARCHAR")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Column(TypeName = "VARCHAR")]
        public string LastName { get; set; }
        [ForeignKey("City")]
        public int  CityID { get; set; }

        public int NumberOfTrips { get; set; }
        [MaxLength(1000)]
        public string SecurityQuestion1 { get; set; }
        [MaxLength(200)]
        public string SecurityAnswer1 { get; set; }
        [MaxLength(1000)]
        public string SecurityQuestion2 { get; set; }
        [MaxLength(200)]
        public string SecurityAnswer2 { get; set; }
        [MaxLength(1000)]
        public string SecurityQuestion3 { get; set; }
        [MaxLength(200)]
        public string SecurityAnswer3 { get; set; }

        //Navigation Properties

        public City City { get; set; }



    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("PlanYourTrip", throwIfV1Schema: false)
        {
        }

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
        }
    }
}
