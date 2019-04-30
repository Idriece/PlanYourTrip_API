using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class HotelDetail
    {
        [Required(ErrorMessage ="HotelName can't be empty")]
        [StringLength(100, ErrorMessage ="Hotel Name can't be greater than 100 char")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "HotelUserId can't be empty")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "HotelCityId can't be empty")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "HotelImage can't be empty")]
        [StringLength(250, ErrorMessage ="Image Name is to Long")]
        public string Image { get; set; }
        public List<RoomPrices>  RoomPrices { get; set; }
}
}