using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    // Class to store package details from different tables in one model
    public class PackageDetails
    {
        public int PackageID { get; set; }
        public string PackageType { get; set; }
        public string PackageName { get; set; }
        public int Days { get; set; }
        public int ProfitPercentage { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public int NumberAvailable { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        public string Image { get; set; }
        public decimal? Rating { get; set; }
    }

    // Class to store itinerary details from different tables
    public class ItineraryDetails
    {
        public int ItineraryID { get; set; }
        public int PackageID { get; set; }
        public int DayNumber { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }
        public int HotelID { get; set; }
        public string Hotel { get; set; }
        public string HotelImage { get; set; }
        public string RoomType { get; set; }
        public decimal RoomPrice { get; set; }
        public string TransportationProviderName { get; set; }
        public string TransportationMode { get; set; }
        public decimal TransportationPrice { get; set; }
        public string ActivityDetails { get; set; }
        public decimal CustomizedRoomPrice { get; set; }
        public decimal CustomizedTransportationPrice { get; set; }
        public int RoomPriceID { get; set; }
        public int CustomizedRoomPriceID { get; set; }
        public int TransportationPriceID { get; set; }
        public int CustomizedTransportationPriceID { get; set; }
    }

    public class HotelDetails
    {
        public int RoomPriceID { get; set; }
        public int PackageID { get; set; }
        public int ItineraryID { get; set; }
        public int HotelID { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
    }

    public class TransportationDetails
    {
        public int TransportationPriceID { get; set; }
        public int PackageID { get; set; }
        public int ItineraryID { get; set; }
        public int TransportationProviderID { get; set; }
        public decimal Price { get; set; }
        public string Mode { get; set; }
    }

    public class PackageManager
    {
        readonly PlanYourTripData db = new PlanYourTripData();

        // SQL query to get all package details along with average rating
        readonly string PackageString =
            "select  Package.PackageID, Package.PackageName, PackageType.Name as PackageType, Package.Days, Package.Price, Package.Summary, Package.NumberAvailable, Package.Image, Package.MinPeople, Package.MaxPeople, convert(decimal(8,1), round(avg(FeedBack.Rating+0.0),2)) as Rating, Package.ProfitPercentage from Package " +
            "join PackageType " +
            "on Package.PackageTypeID = PackageType.PackageTypeID " +
            "left join FeedBack " +
            "on Package.PackageID = FeedBack.PackageID " +
            "group by Package.PackageID, Package.PackageName, PackageType.Name, Package.Days, Package.Price, Package.Summary, Package.NumberAvailable, Package.Image, Package.MinPeople, Package.MaxPeople, Package.ProfitPercentage " +
            "order by Rating desc";

        // SQL query to get all itinerary details
        readonly string ItineraryString =
            "select ItineraryID, PackageID, DayNumber, City.CityID, CityName as City, Hotel.HotelID, HotelName as Hotel, HotelImage, RoomType.Type as RoomType, RoomPrice.Price as RoomPrice, TransportationProvider.TransportationProviderName, TransportationMode.Name as TransportationMode, TransportationPrice.Price as TransportationPrice, ActivityDetails, RoomPrice.Price as CustomizedRoomPrice, TransportationPrice.Price as CustomizedTransportationPrice, RoomPrice.RoomPriceID, RoomPrice.RoomPriceID as CustomizedRoomPriceID, TransportationPrice.TransportationPriceID, TransportationPrice.TransportationPriceID as CustomizedTransportationPriceID from Itinerary " +
            "join " +
            "City " +
            "on Itinerary.CityID = City.CityID " +
            "join " +
            "RoomPrice " +
            "on Itinerary.RoomPriceID = RoomPrice.RoomPriceID " +
            "join RoomType " +
            "on RoomPrice.RoomTypeID = RoomType.RoomTypeID " +
            "join Hotel " +
            "on RoomPrice.HotelID = Hotel.HotelID " +
            "join TransportationPrice " +
            "on Itinerary.TransportationPriceID = TransportationPrice.TransportationPriceID " +
            "join TransportationMode " +
            "on TransportationPrice.TransportationModeID = TransportationMode.TransportationModeID " +
            "join TransportationProvider " +
            "on TransportationPrice.TransportationProviderID = TransportationProvider.TransportationProviderID";
        //"where PackageID = {0}";

        readonly string CustomItineraryString =
            "select [User Customization].CustomizationID as ItineraryID, [User Customization].CustomPackageID as PackageID, [User Customization].DayNumber, City.CityID, CityName as City, Hotel.HotelID, HotelName as Hotel, HotelImage, RoomType.Type as RoomType, RoomPrice.Price as RoomPrice, TransportationProvider.TransportationProviderName, TransportationMode.Name as TransportationMode, TransportationPrice.Price as TransportationPrice, ActivityDetails, RoomPrice.Price as CustomizedRoomPrice, TransportationPrice.Price as CustomizedTransportationPrice, RoomPrice.RoomPriceID, RoomPrice.RoomPriceID as CustomizedRoomPriceID, TransportationPrice.TransportationPriceID, TransportationPrice.TransportationPriceID as CustomizedTransportationPriceID from [User Customization] " +
            "join Itinerary " +
            "on [User Customization].PackageID = Itinerary.PackageID " +
            "join RoomPrice " +
            "on [User Customization].RoomPriceID = RoomPrice.RoomPriceID " +
            "join RoomType " +
            "on RoomPrice.RoomTypeID = RoomType.RoomTypeID " +
            "join Hotel " +
            "on RoomPrice.HotelID = Hotel.HotelID " +
            "join City " +
            "on Hotel.CityID = City.CityID " +
            "join TransportationPrice " +
            "on [User Customization].TransportationPriceID = TransportationPrice.TransportationPriceID " +
            "join TransportationMode " +
            "on TransportationPrice.TransportationModeID = TransportationMode.TransportationModeID " +
            "join TransportationProvider " +
            "on TransportationPrice.TransportationProviderID = TransportationProvider.TransportationProviderID " +
            "where [User Customization].DayNumber = Itinerary.DayNumber " +
            "order by CustomizationID asc;";

        readonly string RoomOptionsString =
            "select RoomPrice.RoomPriceID, Rooms.PackageID, Rooms.ItineraryID, Rooms.HotelID, RoomPrice.Price, RoomType.Type from RoomPrice " +
            "join " +
            "( " +
            "select Itinerary.ItineraryID, RoomPrice.HotelID, Itinerary.PackageID from RoomPrice " +
            "join Itinerary on Itinerary.RoomPriceID = RoomPrice.RoomPriceID " +
            ") as Rooms " +
            "on RoomPrice.HotelID = Rooms.HotelID " +
            "join RoomType " +
            "on RoomPrice.RoomTypeID = RoomType.RoomTypeID";

        readonly string TransportationOptionsString =
            "select TransportationPrice.TransportationPriceID, Transportation.PackageID, Transportation.ItineraryID, Transportation.TransportationProviderID, TransportationPrice.Price, TransportationMode.Name as Mode from TransportationPrice " +
            "join " +
            "( " +
            "select Itinerary.ItineraryID, TransportationPrice.TransportationProviderID, Itinerary.PackageID from TransportationPrice " +
            "join Itinerary on Itinerary.TransportationPriceID = TransportationPrice.TransportationPriceID " +
            ") as Transportation " +
            "on TransportationPrice.TransportationProviderID = Transportation.TransportationProviderID " +
            "join TransportationMode " +
            "on TransportationPrice.TransportationModeID = TransportationMode.TransportationModeID";

        public IEnumerable<string> GetCategories()
        {
            IEnumerable<string> categories = (from category in db.PackageTypes
                                              select category.Name).AsEnumerable();

            return categories;
        }

        // Returns all packages
        public IQueryable GetAllPackages()
        {
            var packages = db.Database.SqlQuery<PackageDetails>(PackageString).AsQueryable();

            var result = from package in packages
                         where package.NumberAvailable > 0
                         select package;

            return result;
        }

        // Return top 5 rated packages
        public IQueryable GetTopPackages()
        {
            var topPackages = db.Database.SqlQuery<PackageDetails>(PackageString).AsQueryable().Take(5);
            
            return topPackages;
        }

        public IEnumerable<PackageDetails> GetSuggestedPackages(string userName)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            string Id = userManager.FindByName(userName).Id;
            
            var packages = db.Database.SqlQuery<PackageDetails>(PackageString).AsEnumerable();

            var suggestedPackages = from package in packages
                                    join interest in db.UserInterests on package.PackageType equals interest.Interest
                                    where interest.Id == Id
                                    orderby package.Rating
                                    select package;

            return suggestedPackages.Union(packages).Take(5);
        }

        // Return itinerary of particular package
        public IQueryable GetItinerary(int packageID)
        {
            var itineraries = db.Database.SqlQuery<ItineraryDetails>(ItineraryString).AsQueryable();

            var result = from itinerary in itineraries
                         where itinerary.PackageID == packageID
                         select itinerary;

            return result;
        }

        public IQueryable GetCustomItineraries(int packageID)
        {
            var itineraries = db.Database.SqlQuery<ItineraryDetails>(CustomItineraryString).AsQueryable();

            var result = from itinerary in itineraries
                         where itinerary.PackageID == packageID
                         select itinerary;

            return result;
        }

        // Return all feedback for a particular package
        public IQueryable GetFeedback(int packageID)
        {
            var feedbacks = from feedback in db.FeedBacks
                            where feedback.PackageID == packageID
                            join user in db.Users on feedback.Id equals user.Id
                            select new
                            {
                                feedback.FeedBackID,
                                feedback.PackageID,
                                user.UserName,
                                feedback.Review,
                                feedback.Rating
                            };

            return feedbacks;
        }

        
        // Return package that matches search term
        public IQueryable SearchPackages(string search)
        {
            var packages = db.Database.SqlQuery<PackageDetails>(PackageString).AsQueryable();
            var itineraries = db.Database.SqlQuery<ItineraryDetails>(ItineraryString).AsQueryable();

            // Store packages where city matches the search term
            var cityPackages = from itinerary in itineraries
                               where itinerary.City.ToLower().Contains(search.ToLower())
                               join package in packages on
                               itinerary.PackageID equals package.PackageID
                               where package.NumberAvailable > 0
                               select package;

            // Store packages where package name matches the search term
            var searchResult = from package in packages
                               where package.NumberAvailable > 0 && (package.PackageName.ToLower().Contains(search.ToLower()) || package.PackageType.ToLower().Equals(search.ToLower()))
                               select package;
            
            // Return a union of all search match results
            return searchResult.Union(cityPackages);
        }      

        // Return details of a particular package
        public IQueryable GetPackage(int packageID)
        {
            var packages = db.Database.SqlQuery<PackageDetails>(PackageString).AsQueryable();

            var package = from pack in packages
                          where pack.PackageID == packageID
                          select pack;

            return package;
        }

        public void DecrementNumAvailable(int packageID, int numPeople)
        {
            Package package = db.Packages.Find(packageID);
            package.NumberAvailable--;
            db.Entry(db.Packages.Find(packageID)).Property("NumberAvailable").IsModified = true;
            db.SaveChanges();
        }
        
        public IQueryable GetRoomOptions(int packageID)
        {
            var roomOptions = db.Database.SqlQuery<HotelDetails>(RoomOptionsString).AsQueryable();

            var result = from options in roomOptions
                         where options.PackageID == packageID
                         orderby options.ItineraryID ascending
                         select options;

            return result;
        }

        public IQueryable GetTransportOptions(int packageID)
        {
            var transportOptions = db.Database.SqlQuery<TransportationDetails>(TransportationOptionsString).AsQueryable();

            var result = from options in transportOptions
                         where options.PackageID == packageID
                         orderby options.ItineraryID ascending
                         select options;

            return result;
        }

        public int AddCustomPackage(CustomPackage package, List<UserCustomization> userCustomizations)
        {
            db.CustomPackages.Add(package);
            db.SaveChanges();
            foreach(UserCustomization itinerary in userCustomizations)
            {
                itinerary.CustomPackageID = package.CustomPackageID;
                db.UserCustomizations.Add(itinerary);
                db.SaveChanges();
            }
            var customID = package.CustomPackageID;
            return customID;
        }
    }
}
