using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("UserCheckIn")]
    public class UserCheckIn
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CheckInID { get; set; }
        [ForeignKey("PackageBooking")]
        public int PackageBookingID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        [ForeignKey("TransportationProvider")]
        public int TransportationProviderID { get; set; }
        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }
        public bool HotelCheckINStatus { get; set; }
        public bool TransportationCheckINStatus { get; set; }

        //Navigation Properties

        public PackageBooking PackageBooking { get; set; }
        public Hotel Hotel { get; set; }
        public TransportationProvider TransportationProvider { get; set; }
    }
}