using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("WishList")]
    public class WishList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WishListID { get; set; }
        [MaxLength(100)]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        [ForeignKey("Package")]
        public int PackageID { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
        public Package Package { get; set; }
    }
}
