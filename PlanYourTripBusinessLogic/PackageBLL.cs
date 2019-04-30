using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessLogic
{
    public class PackageBLL
    {
        PackagesDAL pc = new PackagesDAL();
        public List<Package> GetPackage()
        {
            return pc.GetPackages();
        }
        public bool AddType(PackageType packageType)
        {
            return pc.CreateType(packageType);
        }
        public int AddPackage(Package package)
        {
            
            return pc.AddPackages(package);
        }

        public void AddItinerary(Itinerary itinerary)
        {
            pc.AdditineraryDAL(itinerary);
        }
        public bool EditPackage(Package package)
        {
            return pc.UpdatePackage(package);
        }
        public List<PackageType> PackageTypes()
        {
            return pc.GetPackageTypes();
        }
        public List<string> FetchPackageName()
        {
            return pc.GetPackageNames();
        }

        public List<RoomPriceForAdmin> FetchRoomPrice()
        {
            return pc.GetRoomPrice();
        }
        public List<TransportationPriceForAdmin> FetchTransportationPrice()
        {
            return pc.GetTransportationPrice();
        }
    }
}
