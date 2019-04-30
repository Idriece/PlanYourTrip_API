using Newtonsoft.Json.Linq;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessLogic
{
    
    public class AdminUserManagerLogic
    {
        AdminUserManager adminusermanager = new AdminUserManager();
        /// <summary>
        /// Function to return list of all user details
        /// </summary>
        /// <returns></returns>
        public List<AllUserDetails> LAllUserDetails()
        {
            var value = adminusermanager.AllUserDetails();

            return value;
        }/// <summary>
        /// Function to return count of user, packages, bookings, hotel, transportation
        /// </summary>
        /// <returns></returns>
        public List<CountDTO> LCount()
        {
            return adminusermanager.Count();
        }
        /// <summary>
        /// Function to return list of userdetails by username
        /// </summary>
        /// <param name="IUserName"></param>
        /// <returns></returns>
        public List<AllUserDetails> Luserprofile(string IUserName)
        {
            return adminusermanager.AllUserDetails().Where(x => x.UserName == IUserName).Distinct().ToList();
        }


        public List<UserInterest> GetInterestByID(string id)
        {
            return adminusermanager.GetInterestByID(id);
        }
        /// <summary>
        /// Function to cancel the upcoming packages
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PackageBookingId"></param>
        /// <param name="PackageId"></param>
        public void LCancelBooking(string Id, int PackageBookingId, int PackageId)
        {
            adminusermanager.CancelBooking( Id,  PackageBookingId,  PackageId);
        }
        /// <summary>
        /// Function to return list of booking of a user by name
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<AllBookingUserList> UserPackagesDetails(string UserName)
        {
            return adminusermanager.AllBookingUserList().Where(x=>x.UserName==UserName).ToList();
        }
        /// <summary>
        /// Function to add the user Interest
        /// </summary>
        /// <param name="Interest"></param>
        public void AddInterest(UserInterest Interest)
        {
            adminusermanager.AddInterest(Interest);
        }
        /// <summary>
        /// Function to detete the user Interest
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteInterest(int ID)
        {
            adminusermanager.DeleteInterest(ID);
        }
    }
}
