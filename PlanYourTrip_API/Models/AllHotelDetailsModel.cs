using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class AllHotelDetailsModel
    {
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string CityName { get; set; }
        public string HotelOwnerEmail { get; set; }
        public string HotelImage { get; set; }
        public List<RoomTypePrice> RoomPrices { get; set; }
    }
}