using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("Package")]
    public class Package
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageID { get; set; }
        [ForeignKey("PackageType")]
        public int PackageTypeID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(100)]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        public int Days { get; set; }
        [Display(Name = "Profit Percenatge")]
        public int ProfitPercentage { get; set; }
        [Display(Name = "Total Price")]
        public decimal Price { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(1000)]
        public string Summary { get; set; }
        public int NumberAvailable { get; set; }
        [Display(Name = "Minimum Number Of People")]
        public int MinPeople { get; set; }
        [Display(Name = "Maximum Number Of People")]
        public int MaxPeople { get; set; }
        [MaxLength(1000)]
        public string Image { get; set; }

        //Navigation Properties

        public PackageType PackageType { get; set; }
    }
}
