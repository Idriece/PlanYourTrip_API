using PlanYourTripBusinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class CustomPackageBookingPayment
    {
        public List<UserCustomization> UserCustomizations { get; set; }
        public Booking Booking { get; set; }
        public Payment Payment { get; set; }
    }
}