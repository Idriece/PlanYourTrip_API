using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("CustomPackage")]
    public class CustomPackage
    {
        [Key]
        
        public int CustomPackageID { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "UserId")]
        public string Id { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
    }
}