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
    public class HotelBuisnessLogic
    {
        readonly HotelDAL hotelDAL = new HotelDAL();

        public List<JObject> getAllHotelDetails(string username)
        {
            var allHotelDetails = hotelDAL.getAllHoteldetailDAL(username);
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
            return finalGroupedHotelList;
        }

        public List<JObject> getTodaysCheckIn(int? hotelId)
        {
           var checkIn = hotelDAL.getTodaysCheckInDAL(hotelId);
            List<JObject> todayCheckIn = new List<JObject>();
            foreach(var check in checkIn)
            {
                todayCheckIn.Add(
                    new JObject
                        {
                        new JProperty("CheckInID", check.Item1),
                        new JProperty("PackageBookingID", check.Item2),
                        new JProperty("HotelCheckINStatus", check.Item3),
                        new JProperty("HotelID", check.Item4),
                        new JProperty("TransportationProviderID", check.Item5),
                        new JProperty("NumPeople", check.Item6),
                        new JProperty("PaymentMethod", check.Item7),
                        new JProperty("FirstName", check.Rest.Item1),
                        new JProperty("LastName", check.Rest.Item2),
                        new JProperty("Email", check.Rest.Item3),
                        new JProperty("CheckInDate", check.Rest.Item4),
                        new JProperty("TransportationCheckINStatus", check.Rest.Item5)
                        }
                    );
            }

            return todayCheckIn;
        }

        public List<JObject> getPastCheckIn(int hotelId)
        {
            var checkIn = hotelDAL.getPastCheckInDAL(hotelId);
            List<JObject> pastCheckIn = new List<JObject>();
            foreach(var check in checkIn)
            {
                pastCheckIn.Add(
                    new JObject
                    {
                        new JProperty("HotelName", check.Item1),
                        new JProperty("HotelCheckINStatus", check.Item2),
                        new JProperty("NumPeople", check.Item3),
                        new JProperty("PaymentMethod", check.Item4),
                        new JProperty("FirstName", check.Item5),
                        new JProperty("LastName", check.Item6),
                        new JProperty("Email", check.Item7),
                        new JProperty("CheckInDate", check.Rest.Item1),
                        new JProperty("TransportationProviderName", check.Rest.Item2),
                        new JProperty("PhoneNumber", check.Rest.Item3),
                    });
            }
            return pastCheckIn;
        }

        public bool PutHotelCheckInStatusBL(UserCheckIn userCheckIn)
        {
           return hotelDAL.PutHotelCheckInStatusDAL(userCheckIn);
        }

        public List<JObject> getUpcomingCheckIn(int hotelId)
        {
            var checkIn = hotelDAL.getUpcomingCheckIn(hotelId);
            List<JObject> upComingCheckIn = new List<JObject>();
            foreach (var check in checkIn)
            {
                upComingCheckIn.Add(
                    new JObject
                        {
                        new JProperty("HotelName", check.Item1),
                        new JProperty("HotelCheckINStatus", check.Item2),
                        new JProperty("NumPeople", check.Item3),
                        new JProperty("PaymentMethod", check.Item4),
                        new JProperty("FirstName", check.Item5),
                        new JProperty("LastName", check.Item6),
                        new JProperty("Email", check.Item7),
                        new JProperty("CheckInDate", check.Rest.Item1),
                        new JProperty("TransportationProviderName", check.Rest.Item2),
                        new JProperty("PhoneNumber", check.Rest.Item3)
                        }
                    );
            }

            return upComingCheckIn;

        }
    }
}
