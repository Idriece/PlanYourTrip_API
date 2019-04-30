using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class ItineraryModel
    {
        public int ItineraryID { get; set; }
        public int PackageID { get; set; }
        public IQueryable<decimal> RoomPrice { get; set; }
        public IQueryable<decimal> TransportationPrice { get; set; }
        public IQueryable<string> City { get; set; }
        public string ActivityDetails { get; set; }
        public decimal DayNumber { get; set; }
        public IQueryable<string> HotelName { get; set; }
    }
}