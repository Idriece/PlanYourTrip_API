using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("Payment")]
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        [ForeignKey("PackageBooking")]
        public int PackageBookingID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string CreditCardNumber { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string NameOnCard { get; set; }
        public decimal Amount { get; set; }

        //Navigation Properties

        public PackageBooking PackageBooking { get; set; }
    }
}