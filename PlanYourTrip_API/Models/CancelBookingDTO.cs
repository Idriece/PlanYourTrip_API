using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class CancelBookingDTO
    {
        public string Id { get; set; }
        public int PackageID { get; set; }
        public int PackageBookingId { get; set; }

    }
}