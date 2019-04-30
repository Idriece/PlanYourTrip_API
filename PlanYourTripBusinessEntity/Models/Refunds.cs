using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("Refunds")]
    public class Refunds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefundsId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        [ForeignKey("PackageBooking")]
        public int PackageBookingID { get; set; }
        [ForeignKey("RefundsRules")]
        public int RefundRuleId { get; set; }
        public DateTime RefundDate { get; set; }
        public decimal RefundAmount { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
        public PackageBooking PackageBooking { get; set; }
        public RefundsRules RefundsRules { get; set; }
    }
}
