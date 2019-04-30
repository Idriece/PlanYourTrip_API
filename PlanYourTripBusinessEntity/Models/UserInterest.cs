using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("UserInterest")]
    public class UserInterest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserInterestID { get; set; }

        [ForeignKey("ApplicationUser")]

        public string Id { get; set; }
        [Column(TypeName = "VARCHAR")]
        [MaxLength(20)]
        public string Interest { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
    }
}
