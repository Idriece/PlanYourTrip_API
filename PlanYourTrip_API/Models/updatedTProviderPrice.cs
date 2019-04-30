using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class updatedTProviderPrice
    {
        public decimal Price { get; set; }
        public int TransportationModeID { get; set; }
        public int TransportationPriceID { get; set; }
    }
}