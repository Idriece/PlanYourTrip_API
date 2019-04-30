using PlanYourTripBusinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanYourTripDataAccessLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace PlanYourTripBusinessLogic
{
    public class HotelManagerBuisinessLogic
    {
        readonly AdminHotelManager adminHotelManager = new AdminHotelManager();

       public int AddHotel(Hotel hotel)
        {
            return adminHotelManager.AddHotelToDb(hotel);
        }

        public bool AddNewHotelRoomPrice(RoomPrice roomPrice)
        {
            return adminHotelManager.NewHotelRoomPriceAdder(roomPrice);
        }

        public void AddHotelPrice(List<RoomPrice> roomPrices)
        {
            foreach (RoomPrice price in roomPrices)
            {
                adminHotelManager.AddHotelPrice(price);
            }
        }

        public List<City> getAllCities()
        {
            return adminHotelManager.getAllCities();
        }

        public List<JObject> GetOwnerList()
        {
            var ownerList = adminHotelManager.GetOwnerList();
            var transformedOwnerList = new List<JObject>();

            foreach(var owner in ownerList)
            {
                var ownerTransform = new JObject(
                    new JProperty("Id", owner.Item1),
                    new JProperty("Email", owner.Item2)
                    );
                transformedOwnerList.Add(ownerTransform);
            }

            return transformedOwnerList;
        }

        public List<JObject> GetAllHotelCityListBL()
        {
            return adminHotelManager.GetAllHotelCityListDAL().Select(x => new JObject{
                new JProperty("HotelName", x.Item1),
                new JProperty("CityName", x.Item2)
            }).ToList();
        }

        public List<JObject> getAllHotelDetails()
        {
            //Calling the Method from Data Access Layer
            //Here now we have a list of type List<Tuple<int,string,string,string,string, string, decimal>>
            var allHotelDetails = adminHotelManager.GetAllHotelsDetails();

            //GroupBy will transform the collection into groups, each group has key
            var groupedHotelsByHotelIdList = allHotelDetails
                                                .GroupBy(x => new { x.Item1, x.Item2, x.Item3, x.Item4, x.Item5 })
                                                .ToList();

            List<JObject> finalGroupedHotelList = new List<JObject>();

            //new JProperty("HotelName", grouping.ElementAt(0).Item2) : Initializes a new instance of the JProperty class.
            foreach (var grouping in groupedHotelsByHotelIdList)
            {
                JObject groupedObject = new JObject
                {
                    new JProperty("HotelID", grouping.ElementAt(0).Item1),
                    new JProperty("CityID",grouping.ElementAt(0).Item2),
                    new JProperty("HotelName", grouping.ElementAt(0).Item3),
                    new JProperty("Id",grouping.ElementAt(0).Item4),
                    new JProperty("HotelImage",grouping.ElementAt(0).Item5),
                    new JProperty("Email", grouping.ElementAt(0).Rest.Item2),
                    new JProperty("CityName", grouping.ElementAt(0).Rest.Item3)
                };

                List<JObject> roomTypeObjectList = new List<JObject>();

                foreach (var groupedElement in grouping)
                    roomTypeObjectList.Add(new JObject
                    {
                        new JProperty("RoomTypeName", groupedElement.Rest.Item4),
                        new JProperty("RoomTypeID", groupedElement.Item7),
                        new JProperty("Price", groupedElement.Rest.Item1),
                        new JProperty("RoomPriceID",groupedElement.Item6)
                    });

                groupedObject.Add("RoomPrices", JToken.FromObject(roomTypeObjectList));
                finalGroupedHotelList.Add(groupedObject);
            }

            //SerializeObject of JsonCOnvert Class it returns json format text
            //jsonSerializer setting list .https://www.newtonsoft.com/json/help/html/SerializationSettings.htm
            //contract resolver governs serialization / deserialization behavior at a broad level
            // if you want to customize some aspect of serialization or deserialization across a wide range of classes, you will probably need to use a ContractResolver to do it. Here are some examples of things you can customize using a ContractResolver:
            //.https://stackoverflow.com/questions/41088492/json-net-contractresolver-vs-jsonconverter
            //formatting Enumeration Specifies formatting options for the JsonTextWriter.
            //turns on indentation
            //string serializedGroupedHotelList = JsonConvert.SerializeObject(finalGroupedHotelList, new JsonSerializerSettings
            //{
            //    ContractResolver = new DefaultContractResolver(),
            //    Formatting = Formatting.Indented
            //});

            return finalGroupedHotelList;            
        }

        public bool PutHotelDetail(Hotel hotel)
        {
           return adminHotelManager.updateHotelDetail(hotel);
        }
        public void PutHotelPrice(List<RoomPrice> roomPrices)
        {
            foreach(RoomPrice roomPrice in roomPrices)
            {
                adminHotelManager.updateHotelPrice(roomPrice);
            }
        }

        public List<AllBookingUserList> LPackageBooking(DateTime Start, DateTime End)
        {
            return adminHotelManager.PackageBooking(Start,End);
        }
    }
}
