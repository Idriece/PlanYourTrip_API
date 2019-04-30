using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlanYourTripBusinessLogic;
using System.Security.Claims;
using PlanYourTripDataAccessLayer.Context;
using System.Threading.Tasks;
using System.Web;
using PlanYourTrip_API.Service;
using System.Net.Http.Headers;
using System.IO;
using System.Text;

namespace PlanYourTrip_API.Controllers
{
   
 public class AccountController : ApiController
    {
        PlanYourTripData db = new PlanYourTripData();
        List<UserDetail> userdetail;
        UserDetail Objuserdetail = new UserDetail();
        EditUserProfileBLL epb = new EditUserProfileBLL();
        protected UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());

        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> RegisterAsync(AccountModel model)
        {
            try
            {

                if (model == null)
                {
                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, "Model is null, please check request");
                }

                if (!ModelState.IsValid)
                {
                    string modelErrorMessage = string.Join(" | ", ModelState.Values
                                                        .SelectMany(v => v.Errors)
                                                        .Select(e => e.ErrorMessage));

                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, modelErrorMessage);
                }


                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var manager = new UserManager<ApplicationUser>(userStore);
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    CityID = model.CityID

                };

                manager.PasswordValidator = new PasswordValidator
                {
                    RequiredLength = 3
                };

                IdentityResult result = manager.Create(user, model.Password);

                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "NormalUser");

                    ////send confirmation email

                    //var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("PlanYourTrip");
                    //manager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));

                    //var code = await manager.GenerateEmailConfirmationTokenAsync(user.Id);
                   
                    //var urlLink = Url.Link("Default", new { controller="Account",action="verify-email",id = user.Id, token = code });

                    //StringBuilder body = new StringBuilder();
                    //body.Append(@"<a href='" + urlLink + "'>Click Here To Verify your Account</a>");
                    //IdentityMessage myMessage = new IdentityMessage()
                    //{
                    //    Body = body.ToString(),
                    //    Destination = user.Email,
                    //    Subject = "Confirm your mail"
                    //};
                    //EmailService sendEmail = new EmailService();
                    //await sendEmail.SendAsync(myMessage);

                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.Created, "User added");

                }

                string identityErrorMessage = string.Join("\n", result.Errors
                                                       .Select(x => x));
                return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, identityErrorMessage);
            }
            catch (Exception ex)
            {
                return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.InternalServerError, "Failed because: {ex.Message}");
            }
        }

        // To verify email of user using id and token
        //[Route("Account/verify-email")]
        //[HttpGet]
        //public async Task<HttpResponseMessage> VerifyEmail(string id, string token)
        //{
        //    var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
        //    var manager = new UserManager<ApplicationUser>(userStore);
        //    var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("PlanYourTrip");
        //    manager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
        //    IdentityResult result = await manager.ConfirmEmailAsync(id, token);
        //    if (result.Succeeded)
        //    {
        //        var user = manager.FindById(id);
        //        user.EmailConfirmed = true;
        //        manager.UpdateAsync(user);
        //        IdentityMessage myMessage = new IdentityMessage()
        //        {
        //            Body = "Email Confirmed",
        //            Destination = user.Email,
        //            Subject = "Your Account is verified"
        //        };
        //        EmailService sendEmail = new EmailService();
        //        await sendEmail.SendAsync(myMessage);
        //        var fileContents = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/EmailConfirmation.html"));
        //        var response = new HttpResponseMessage()
        //        {
        //            Content = new StringContent(fileContents)
        //        };
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
        //        return response;

        //    }
        //    else
        //    {
        //        string identityErrorMessage = string.Join("\n", result.Errors.Select(x => x));
        //        return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, identityErrorMessage);
        //    }
        //}


        // code to register or login user using social login
        [Route("api/ExternalLogin")]
        [HttpPost]
        public async Task<HttpResponseMessage> ExternalLoginAsync(SocialLoginDTO userData)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var manager = new UserManager<ApplicationUser>(userStore);
            var existingUser = manager.FindByEmail(userData.Email);
            if (existingUser == null)
            {
                var user = new ApplicationUser();
                user.UserName = (userData.Email).Split('@')[0];
                user.FirstName = userData.FirstName;
                user.LastName = userData.LastName;
                user.Email = userData.Email;
                user.EmailConfirmed = true;
                user.CityID = userData.CityId;
                IdentityResult result = manager.Create(user);
                if (result.Succeeded)
                {
                    manager.AddToRole(user.Id, "NormalUser");
                    var userLoginInfo = new UserLoginInfo(userData.Provider, userData.Id);
                    manager.AddLogin(user.Id, userLoginInfo);
                    List<string> rolename = manager.GetRoles(user.Id).ToList();
                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.OK, rolename);
                }
                return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, "Failed");
            }
            else
            {
                var userLogIn = manager.Find(new UserLoginInfo(userData.Provider, userData.Id));
                if (userLogIn != null)
                {
                    List<string> rolename = manager.GetRoles(userLogIn.Id).ToList();
                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.OK, rolename);
                }
                else
                {
                    return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, "login Failed");
                }
            }
        }


        // to get email of users
        [Route("api/User/Email")]
        [HttpGet]
        [AllowAnonymous]
        public List<string> GetEmail()
        {

            try
            {

                return (userStore.Users.Select(m => m.Email)).ToList<string>();

            }
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.Ambiguous);
            }

        }
        /// <summary>
        /// View Profile
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/RecoveryId")]
        public ApplicationUser GetUserDetails(string emailId)
        {
            
            ApplicationUser RecoveryId = db.Users.SingleOrDefault(x => x.Email == emailId);
             return RecoveryId;
        }
        /// <summary>
        /// Change a Password of ForgetPassWord Users
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("api/forget/changepassword")]
        public async Task<HttpResponseMessage> ChangePasswordAsync(AccountModel model)
        {
            try
            {
                string emailId=model.Email;
                string password=model.Password;
                ApplicationUser RecoveryId = db.Users.SingleOrDefault(x => x.Email == emailId);

                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var manager = new UserManager<ApplicationUser>(userStore);

                var currentUser = manager.FindByEmail(emailId);
                var newPasswordHash = manager.PasswordHasher.HashPassword(password);
                await userStore.SetPasswordHashAsync(currentUser, newPasswordHash);
                await manager.UpdateAsync(currentUser);

                return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.Accepted, "New Password Updated");
            }
            catch(Exception ex)
            {
                return HTTPBusinessLogic.SetHttpResponse(HttpStatusCode.BadRequest, "Failed to update new Password");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/ForAdminRole")]
        public string ForAdminRole()
        {
            return "for admin role";
        }

        [HttpGet]
        [Authorize(Roles = "NormalUser")]
        [Route("api/ForNormalUserRole")]
        public string ForNormalUserRole()
        {
            return "For normal user role";
        }

        [HttpGet]
        [Authorize(Roles = "HotelOwner")]
        [Route("api/ForHotelOwnerRole")]
        public string ForHotelOwnerRole()
        {
            return "For hotel owner role";
        }

        [HttpGet]
        [Authorize(Roles = "TransportationProvider")]
        [Route("api/ForTransportationProviderRole")]
        public string ForTransportationProviderRole()
        {
            return "For transporation provider role";
        }

        /// <summary>
        /// Function to return the list of user and the roles
        /// </summary>
        /// <returns></returns>

        [Route("api/User/Users")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetUsers()
        {
            try
            {
                var manager = new UserManager<ApplicationUser>(userStore);
                var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var roleList = roleManager.Roles.ToList();
                var userList = userStore.Users.ToList();


                userdetail = (from rl in roleList
                              join ul in userList on rl.Id equals ul.Roles.FirstOrDefault().RoleId
                              select new UserDetail { FirstName = ul.FirstName, LastName = ul.LastName, UserName = ul.UserName, Email = ul.Email, Id = ul.Id, Role = rl.Name, RoleId = rl.Id }).ToList();
            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Conflit in Entries"));
            }
            return Ok(userdetail);
        }
        /// <summary>
        ///  Function to change the roles of user
        /// </summary>
        /// <param name="changerole"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/User/Users")]
        public async Task<IHttpActionResult> Edit(ChangeRole changerole)
        {
            try
            {
                ApplicationDbContext applicationDbContext = new ApplicationDbContext();
                ApplicationUser user = applicationDbContext.Users.FirstOrDefault(x => x.Id == changerole.Id);
                var manager = new UserManager<ApplicationUser>(userStore);
                var oldRoleId = user.Roles.FirstOrDefault().RoleId;
                var oldRole = applicationDbContext.Roles.Where(x => x.Id == oldRoleId).FirstOrDefault();

                if (user == null)
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User not found with ID {changerole.Id}"));

                IdentityResult result = manager.RemoveFromRole(changerole.Id, oldRole.Name);
                if (result.Succeeded)
                    manager.AddToRole(changerole.Id, changerole.NewRole);

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Accepted, "User updated with ID"));

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Updation of user failed because: {ex.Message}"));
            }
        }
        /// <summary>
        /// Edit user profile 
        /// </summary>
        /// <param name="userprofileDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/user/edit")]
        public async Task<IHttpActionResult> EditUserProfile(UserProfileDTO userprofileDTO)
        {
            try
            {
                var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
                var manager = new UserManager<ApplicationUser>(userStore);
                var currentUser = manager.FindById(userprofileDTO.UserId);
                currentUser.FirstName = userprofileDTO.FirstName;
                currentUser.LastName = userprofileDTO.LastName;
                currentUser.PhoneNumber = userprofileDTO.PhoneNumber;
                var result = await manager.UpdateAsync(currentUser);
                var ctx = userStore.Context;
                ctx.SaveChanges();
                if (result.Succeeded)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Accepted, "User profile Updated"));
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "User profile Failed"));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "User profile Failed"));
            }
        }
    }
}
