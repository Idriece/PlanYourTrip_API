
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using System.Web.Http.Description;
using System.Threading.Tasks;

namespace PlanYourTrip_API.Controllers
{
    public class AdminUserController : ApiController
    {
        AdminUserManagerLogic adminusermanagerlogic = new AdminUserManagerLogic();
        /// <summary>
        /// Function to return list of user details with a list interset, packagename and packagtype. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/user/userlist")]
        public async Task<IHttpActionResult> CAllUserDetailsAsync()
        {
            try
            {
                var value = adminusermanagerlogic.LAllUserDetails().GroupBy(m => m.Email);

                List<ListAllUserDetails> allUserDetail = new List<ListAllUserDetails>();


                foreach (var grouping in value)
                {
                    int packagecount = 0;
                    ListAllUserDetails groupedObject = new ListAllUserDetails
                    {
                        FirstName = grouping.ElementAt(0).FirstName,
                        LastName = grouping.ElementAt(0).LastName,
                        UserName = grouping.ElementAt(0).UserName,
                        UserId = grouping.ElementAt(0).UserId,
                        NumberOfTrips = grouping.ElementAt(0).NumberOfTrips,
                        Email = grouping.ElementAt(0).Email,
                        LoginProvider = grouping.ElementAt(0).LoginProvider == null ? "Registered User" : grouping.ElementAt(0).LoginProvider,
                        City = grouping.ElementAt(0).City,
                        State = grouping.ElementAt(0).State,
                        Role = grouping.ElementAt(0).Role,
                        Interests = new List<InterestDTO>(),
                        PackageType = new List<PackageTypeDTO>(),
                        Packages = new List<PackageDTO>()
                    };

                    foreach (var groupedElement in grouping)
                    {

                        if (groupedElement.Interest != null)
                            groupedObject.Interests.Add(
                        new InterestDTO()
                        {
                            UserInterestID = groupedElement.UserInterestID,
                            UserInterest = groupedElement.Interest
                        });

                    }
                    foreach (var groupedPackage in grouping)
                    {
                        if (groupedPackage.PackageName != null)
                            groupedObject.Packages.Add(
                            new PackageDTO()
                            {
                                PackageName = groupedPackage.PackageName
                            });
                        if (groupedPackage.PackageName != null)
                            packagecount += 1;
                    }
                    foreach (var groupedPackageType in grouping)
                    {
                        if (groupedPackageType.PackageType != null)
                            groupedObject.PackageType.Add(
                            new PackageTypeDTO()
                            {
                                PackageTypes = groupedPackageType.PackageType
                            });
                    }

                    allUserDetail.Add(groupedObject);
                }
                return Ok(allUserDetail);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// Function to return a count of Registered user, Package, Hotel owner, Transport Provider.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/admin/usercount")]
        public async Task<IHttpActionResult> CCount()
        {
            try
            {
                return Ok(adminusermanagerlogic.LCount());
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }

        }
        /// <summary>
        /// Function to return a detail of a Exact user details by UserId as a input
        /// </summary>
        /// <param name="IUserName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/viewprofile")]
        public async Task<IHttpActionResult> Cuserprofile(string IUserName)
        {
            try
            {

                var value = adminusermanagerlogic.Luserprofile(IUserName).GroupBy(m => m.Email);
                ListAllUserDetails groupedObject = new ListAllUserDetails();
                foreach (var grouping in value)
                {
                    int packagecount = 0;
                    groupedObject.FirstName = grouping.ElementAt(0).FirstName;
                    groupedObject.LastName = grouping.ElementAt(0).LastName;
                    groupedObject.UserName = grouping.ElementAt(0).UserName;
                    groupedObject.UserId = grouping.ElementAt(0).UserId;
                    groupedObject.NumberOfTrips = grouping.ElementAt(0).NumberOfTrips;
                    groupedObject.Email = grouping.ElementAt(0).Email;
                    groupedObject.LoginProvider = grouping.ElementAt(0).LoginProvider == null ? "Registered User" : "";
                    groupedObject.City = grouping.ElementAt(0).City;
                    groupedObject.State = grouping.ElementAt(0).State;
                    groupedObject.Role = grouping.ElementAt(0).Role;
                    groupedObject.PhoneNumber = grouping.ElementAt(0).PhoneNumber;
                   
                    groupedObject.Interests = new List<InterestDTO>();
                    groupedObject.PackageType = new List<PackageTypeDTO>();
                    groupedObject.Packages = new List<PackageDTO>();

                    foreach (var groupedElement in grouping)
                    {
                        if (groupedElement.Interest != null)
                            groupedObject.Interests.Add(
                        new InterestDTO()
                        {
                            UserInterestID = groupedElement.UserInterestID,
                            UserInterest = groupedElement.Interest
                        });
                    }

                    foreach (var groupedPackage in grouping)
                    {
                        if (groupedPackage.PackageName != null)
                            groupedObject.Packages.Add(
                            new PackageDTO()
                            {
                                PackageName = groupedPackage.PackageName
                            });
                        if (groupedPackage.PackageName != null)
                            packagecount += 1;
                    }

                    foreach (var groupedPackageType in grouping)
                    {
                        if (groupedPackageType.PackageType != null)
                            groupedObject.PackageType.Add(
                            new PackageTypeDTO()
                            {
                                PackageTypes = groupedPackageType.PackageType

                            });
                    }
                }
                return Ok(groupedObject);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// Function to Fetch the User Interests
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/FetchInterest")]
        public async Task<IHttpActionResult> GetInterestByID(string id)
        {
            try
            {
                var interestToRetrieve =  adminusermanagerlogic.GetInterestByID(id);
                    return Ok(interestToRetrieve);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// Function to perform the update operation of a specific user by UserId as a input
        /// </summary>
        /// <param name="userprofileDTO"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/forgetpassword/otp")]
        public async Task<IHttpActionResult> OTP(string EmailId)
        {
            if(EmailId==null)
            {
                return BadRequest("Provide a Valid Email");
            }
            try
            {
                string num = "0123456789";
                int len = num.Length;
                string otp = string.Empty;
                int otpdigit = 5;
                string finaldigit;
                int getindex;
                for (int i = 0; i < otpdigit; i++)
                {
                    do
                    {
                        getindex = new Random().Next(0, len);
                        finaldigit = num.ToCharArray()[getindex].ToString();
                    } while (otp.IndexOf(finaldigit) != -1);
                    otp += finaldigit;

                }
                String emailbody = "Your OTP is : " + otp;

                MailMessage mailMessage = new MailMessage("csbbatch2014@gmail.com", EmailId);
                mailMessage.Body = emailbody;

                mailMessage.Subject = "Plan Your Trip Verification";
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Send(mailMessage);

                return Ok(otp);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// Cancel booking and it calculate refund amount
        /// </summary>
        /// <param name="cancelBookingDTO"></param>
        [HttpPut]
        [Route("api/user/cancelbooking")]
        public async Task<IHttpActionResult> CCancelBooking(CancelBookingDTO cancelBookingDTO)
        {
            try
            {
                adminusermanagerlogic.LCancelBooking(cancelBookingDTO.Id, cancelBookingDTO.PackageBookingId, cancelBookingDTO.PackageID);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// To return the list of booking upcoming of a user
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/upcomingbookinglist")]
        public async Task<IHttpActionResult> UserUpcomingPackagesDetails(string UserName)
        {
            try
            {
                var value = adminusermanagerlogic.UserPackagesDetails(UserName).Where(x => x.StartDate >= DateTime.Now).ToList();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// To return the list of booking current of a user
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/bookinglist")]
        public async Task<IHttpActionResult> UserPackagesDetails(string UserName)
        {
            try
            {
                var value = adminusermanagerlogic.UserPackagesDetails(UserName).Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).ToList();
                return Ok(value);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// To return the list of booking history of a user
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/historybookinglist")]
        public async Task<IHttpActionResult> UserHistoryPackagesDetails(string UserName)
        {
            try
            {
                var value = adminusermanagerlogic.UserPackagesDetails(UserName).Where(x => x.EndDate <= DateTime.Now).ToList();
                return Ok(value);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }
        /// <summary>
        /// Add Interest in View Profile
        /// </summary>
        /// <param name="Interest"></param>
        [HttpPost]
        [Route("api/user/addinterest")]
        public async Task<IHttpActionResult> AddInterest(UserInterest Interest)
        {
            try
            {
                adminusermanagerlogic.AddInterest(Interest);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }

        }
        /// <summary>
        /// Delete Interest in View Profile
        /// </summary>
        /// <param name="ID"></param>
        [HttpPut]
        [Route("api/user/deleteinterest")]
        public async Task<IHttpActionResult> DeleteInterest(int ID)
        {
            try
            {
                adminusermanagerlogic.DeleteInterest(ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error Occured Internally"));
            }
        }

    }
}