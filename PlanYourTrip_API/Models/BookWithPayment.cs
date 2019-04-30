using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class BookWithPayment
    {
        public Booking booking { get; set; }
        public PaymentModel payment { get; set; }
    }
}