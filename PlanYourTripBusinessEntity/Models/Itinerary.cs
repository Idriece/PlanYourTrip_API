using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("Itinerary")]
    public class Itinerary
    {
        [Key]
        public int ItineraryID { get; set; }
        [ForeignKey("Package")]
        public int PackageID { get; set; }
        [ForeignKey("RoomPrice")]
        public int RoomPriceID { get; set; }
        [ForeignKey("TransportationPrice")]
        public int TransportationPriceID { get; set; }
        public int CityID { get; set; }
        [Column(TypeName = "NVARCHAR")]
        [StringLength(450)]
        public string ActivityDetails { get; set; }
        public int DayNumber { get; set; }

        //Navigation Properties

        public Package Package { get; set; }
        public RoomPrice RoomPrice { get; set; }
        public TransportationPrice TransportationPrice { get; set; }       
    }
}