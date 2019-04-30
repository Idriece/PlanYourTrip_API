using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class itineraries
    {
        public int RoomPriceID { get; set; }
        public int TransportationPriceID { get; set; }
        public int city { get; set; }
        public string activity { get; set; }
        public int day { get; set; }
    }
}