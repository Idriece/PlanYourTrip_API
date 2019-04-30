using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class CreatePackage
    {
        public int packagetypeid { get; set; }
        public string packageName { get; set; }
        public int days { get; set; }
        public int profitPercentage { get; set; }
        public decimal Price { get; set; }
        public string summary { get; set; }
        public int numAvailable { get; set; }
        public int minimumPeople { get; set; }
        public int maximumPeople { get; set; }
        public string Image { get; set; }
        public List<itineraries> itineraries { get; set; }
    }
}