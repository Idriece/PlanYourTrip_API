using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class UpdatedHotelPriceDTO
    {
        public int RoomPriceID { get; set; }
        public int RoomTypeID { get; set; }
        public decimal Price { get; set; }
    }
}