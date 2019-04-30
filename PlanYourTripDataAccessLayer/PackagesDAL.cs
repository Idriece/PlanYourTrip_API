using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class PackagesDAL
    {
        PlanYourTripData db = new PlanYourTripData();
     

        //Function to list all packages available
        public List<Package> GetPackages()
        {
                return db.Packages.Include("PackageType").ToList();
        }
        //function to list all packageName 
        public List<string> GetPackageNames()
        {
            var query = db.Packages.Select(x => x.PackageName);
            return query.ToList(); 
        }
        public bool CreateType(PackageType packageType)
        {
            db.PackageTypes.Add(packageType);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
                throw new Exception();
            }
            return true;
        }
        //function to add package into database 
        public int AddPackages(Package package)
        {
            db.Packages.Add(package);
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw new Exception();
            }
            int id = db.Packages.Max(x => x.PackageID);
            return id;
        }
        //function to add itinerary details into database
        public void AdditineraryDAL(Itinerary itinerary)
        {
            db.Itineraries.Add(itinerary);
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                throw new Exception();
            }
        }
        //function to update package details 
        public bool UpdatePackage(Package package)
        {
            var associatedDetails = db.Packages.Where(x => x.PackageID == package.PackageID).FirstOrDefault();
            if (associatedDetails != null)
            {
                package.Days = associatedDetails.Days;
                package.ProfitPercentage = associatedDetails.ProfitPercentage;
                package.Price = associatedDetails.Price;
                package.Image = associatedDetails.Image;
            }
            try
            {
                db.Entry(associatedDetails).CurrentValues.SetValues(package);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
                throw new Exception();
            }
            return true;
        }
        public List<PackageType> GetPackageTypes()
        {
            return db.PackageTypes.ToList();
        }
        public List<RoomPriceForAdmin> GetRoomPrice()
        {
            return db.RoomPrices.Select(p => new RoomPriceForAdmin
            {
                RoomPriceID = p.RoomPriceID,
                RoomPrice = p.Price
            }).ToList();
        }
        public List<TransportationPriceForAdmin> GetTransportationPrice()
        {
            return db.TransportationPrices.Select(p => new TransportationPriceForAdmin
            {
                TransportationPriceID = p.TransportationPriceID,
                TransportationPrice = p.Price
            }).ToList();
        }
    }
}
