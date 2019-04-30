using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class EditPackageDTO
    {
        public int packageid { get; set; }
        public int maximumPeople { get; set; }
        public int minimumPeople { get; set; }
        public int numAvailable { get; set; }
        public string packageName { get; set; }
        public int packagetypeid { get; set; }
        public string summary { get; set; }
    }
}