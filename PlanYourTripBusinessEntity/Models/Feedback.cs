using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("FeedBack")]
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FeedBackID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        [ForeignKey("Package")]
        public int PackageID { get; set; }

        public int Rating { get; set; }
        [MaxLength(2000)]
        public string Review { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
        public Package Package { get; set; }
    }
}