using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("PackageBooking")]
    public class PackageBooking
    {
        [Key]
        public int PackageBookingID { get; set; }
        public int PackageID { get; set; }
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public int NumPeople { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        public bool IsCustomized { get; set; }
        public decimal TotalCost { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        public string BookingStatus { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
        public Package Package { get; set; }
    }
}