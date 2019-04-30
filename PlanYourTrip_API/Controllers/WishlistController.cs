using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PlanYourTrip_API.Controllers
{
    public class WishlistController : ApiController
    {
        readonly WishlistManager wishlistManager = new WishlistManager();

        // Check if wishlist entry exists for given user name and package ID
        [HttpGet]
        [Route("api/packages/inWishlist/{userName}/{packageID}")]
        public bool InWishlist(string userName, int packageId)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            string id = userManager.FindByName(userName).Id;
            return wishlistManager.InWishlist(id, packageId);
        }

        // Add package to wishlist for corresponsing user
        [HttpPost]
        [Route("api/packages/addToWishlist/")]
        public void AddWishlist([FromBody] Wishlist wishlist)
        {
            WishList dbWishlist = new WishList();
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            dbWishlist.Id = userManager.FindByName(wishlist.UserName).Id;
            dbWishlist.PackageID = wishlist.PackageId;
            wishlistManager.AddWishlist(dbWishlist);
        }

        // Remove package from wishlist for corresponding user
        [HttpDelete]
        [Route("api/packages/removeFromWishlist/{userName}/{packageID}")]
        public void RemoveFromWishlist(string userName, int packageId)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            string id = userManager.FindByName(userName).Id;
            wishlistManager.RemoveWishlist(id, packageId);
        }

        // Get list of package ID's on wishlist for given username
        [HttpGet]
        [Route("api/packages/getWishlist")]
        public int[] GetWishlist(string userName)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            string id = userManager.FindByName(userName).Id;
            return wishlistManager.GetWishlist(id);
        }
    }
}
