using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class HotelUserList
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public String LastName { get; set; }
        public string UserEmail { get; set; }
        public List<RoomTypePrice> RoomTypePrices { get; set; }
        public List<ItineraryModel> Itineraries { get; set; }
        //public List<PackageBooking>
    }
}