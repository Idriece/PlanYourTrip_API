using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class WishlistManager
    {
        readonly PlanYourTripData db = new PlanYourTripData();

        // Check if wishlist entry exists for given user and package ID
        public bool InWishlist(string id, int packageId)
        {
            var wishlistDB = db.WishLists.AsQueryable();
            var result = from entries in wishlistDB
                         where entries.Id == id && entries.PackageID == packageId
                         select entries;
            return result.Count() != 0;
        }

        // Add entry to wishlist table
        public void AddWishlist(WishList wishlist)
        {
            db.WishLists.Add(wishlist);
            db.SaveChanges();
        }

        // Remove entry from wishlist database
        public void RemoveWishlist(string id, int packageId)
        {
            var wishlistDB = db.WishLists.AsQueryable();
            var result = from entries in wishlistDB
                         where entries.Id == id && entries.PackageID == packageId
                         select entries;
            foreach (WishList w in result)
            {
                db.WishLists.Remove(w);
            }
            db.SaveChanges();
        }

        // Get all entries of wishlist for given user
        public int[] GetWishlist(string id)
        {
            var wishlistDB = db.WishLists.AsQueryable();
            var result = from entries in wishlistDB
                         where entries.Id == id
                         select entries.PackageID;
            return result.ToArray();
        }
    }
}
