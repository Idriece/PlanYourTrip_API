using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class UpdateTProviderDTO
    {
        public int TransportationProviderID { get; set; }
        public string Id { get; set; }
        public int CityID { get; set; }
        public string TransportationProviderName { get; set; }
        public List<updatedTProviderPrice> updatedTProviderPrice { get; set; }
    }
}