using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class AddTProviderDTO
    {
        public string TransportationProviderName { get; set; }
        public string UserId { get; set; }
        public int CityId { get; set; }
        public List<TransportationPriceDTO> TransportationPrices { get; set; }
    }
}