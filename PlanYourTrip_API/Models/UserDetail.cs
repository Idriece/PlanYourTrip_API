using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class UserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
        //public int RoleId { get; set; }
        public string Role { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; internal set; }
    }
}