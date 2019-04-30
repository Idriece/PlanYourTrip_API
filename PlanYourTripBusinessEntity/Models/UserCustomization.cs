using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("User Customization")]
    public class UserCustomization
    {
        [Key]
        public int CustomizationID { get; set; }
        [ForeignKey("CustomPackage")]
        public int CustomPackageID { get; set; }
        [ForeignKey("Package")]
        public int PackageID { get; set; }
        [ForeignKey("RoomPrice")]
        public int RoomPriceID { get; set; }
        [ForeignKey("TransportationPrice")]
        public int TransportationPriceID { get; set; }
        public int DayNumber { get; set; }

        //Navigation Properties

        public CustomPackage CustomPackage { get; set; }
        public Package Package { get; set; }
        public TransportationPrice TransportationPrice { get; set; }
        public RoomPrice RoomPrice { get; set; }
    }
}
