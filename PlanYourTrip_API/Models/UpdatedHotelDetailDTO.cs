using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class UpdatedHotelDetailDTO
    {
        [Required(ErrorMessage ="Id for hotel is required")]
        public int HotelID { get; set; }
        [Required(ErrorMessage = "Please provide a city")]
        public int CityID { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Hotel Name should be between 1 to 100 chars")]
        public string HotelName { get; set; }
        [Required(ErrorMessage ="Email ID is a required feild. Please provide it.")]
        public string Id { get; set; }
        [Required]
        public string HotelImage { get; set; }
        public List<UpdatedHotelPriceDTO> updatedHotelPriceDTO { get; set; }
    }
}