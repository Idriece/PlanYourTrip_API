using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class PaymentModel
    {
        public int BookingID { get; set; }
        public string CreditCardNumber { get; set; }
        public string NameOnCard { get; set; }
        public decimal Amount { get; set; }
    }
}