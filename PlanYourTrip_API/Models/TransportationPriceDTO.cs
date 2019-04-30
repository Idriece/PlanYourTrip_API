using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class TransportationPriceDTO
    {
        public int TransportationModeID { get; set; }
        public decimal Price { get; set; }
    }
}