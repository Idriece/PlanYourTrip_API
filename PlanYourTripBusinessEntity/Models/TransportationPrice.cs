using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("TransportationPrice")]
    public class TransportationPrice
    {
        [Key]
        public int TransportationPriceID { get; set; }
        [ForeignKey("TransportationProvider")]
        public int TransportationProviderID { get; set; }
        [ForeignKey("TransportationMode")]
        public int TransportationModeID { get; set; }
        public decimal Price { get; set; }

        //Navigation Properties

        public TransportationProvider TransportationProvider { get; set; }
        public TransportationMode TransportationMode { get; set; }
    }
}