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
    public class TransportBuisnessLogic
    {
        TransportDAL transportDAL = new TransportDAL();

        public List<JObject> getAllTransportDetails(string username)
        {
            var allTransportDetails = transportDAL.GetAllTProviderDetailsDAL(username);

            var groupedTransportationProvider = allTransportDetails
                                                .GroupBy(x => new { x.Item1, x.Item2, x.Item3, x.Item4 }).ToList();

            List<JObject> finalGroupedTransportationList = new List<JObject>();

            foreach (var grouping in groupedTransportationProvider)
            {
                JObject groupedObject = new JObject
                {
                    new JProperty("TransportationProviderID",grouping.ElementAt(0).Item1),
                    new JProperty("CityID", grouping.ElementAt(0).Item2),
                    new JProperty("TransportationProviderName", grouping.ElementAt(0).Item3),
                    new JProperty("Id", grouping.ElementAt(0).Item4),
                    new JProperty("Email", grouping.ElementAt(0).Rest.Item1),
                    new JProperty("CityName", grouping.ElementAt(0).Rest.Item2)

                };

                List<JObject> transportPriceObjectList = new List<JObject>();

                foreach (var groupedElement in grouping)
                    transportPriceObjectList.Add(new JObject
                    {
                        new JProperty("TransportationPriceID", groupedElement.Item5),
                        new JProperty("TransportationModeID", groupedElement.Item6),
                        new JProperty("Name", groupedElement.Rest.Item3),
                        new JProperty("Price", groupedElement.Item7)
                    });

                groupedObject.Add("TransportationPrices", JToken.FromObject(transportPriceObjectList));
                finalGroupedTransportationList.Add(groupedObject);
            }

            return finalGroupedTransportationList;
        }
        public List<JObject> getTodaysCheckIn(int transportproviderId)
        {
            var checkIn = transportDAL.getTodaysCheckInDAL(transportproviderId);
            List<JObject> todayCheckIn = new List<JObject>();
            foreach (var check in checkIn)
            {
                todayCheckIn.Add(
                    new JObject
                        {
                        new JProperty("CheckInID", check.Item1),
                        new JProperty("PackageBookingID", check.Item2),
                        new JProperty("HotelCheckINStatus", check.Rest.Item5),
                        new JProperty("HotelID", check.Item4),
                        new JProperty("TransportationProviderID", check.Item5),
                        new JProperty("NumPeople", check.Item6),
                        new JProperty("PaymentMethod", check.Item7),
                        new JProperty("FirstName", check.Rest.Item1),
                        new JProperty("LastName", check.Rest.Item2),
                        new JProperty("Email", check.Rest.Item3),
                        new JProperty("CheckInDate", check.Rest.Item4),
                        new JProperty("TransportationCheckINStatus", check.Item3),
                        }
                    );
            }

            return todayCheckIn;
        }

        public List<JObject> getPastCheckIn(int transportproviderId)
        {
            var checkIn = transportDAL.getPastCheckInDAL(transportproviderId);
            List<JObject> pastTransportCheckIns = new List<JObject>();
            foreach(var check in checkIn)
            {
                pastTransportCheckIns.Add(
                    new JObject
                    {
                        new JProperty("HotelName", check.Item1),
                        new JProperty("TransportationCheckINStatus", check.Item2),
                        new JProperty("NumPeople", check.Item3),
                        new JProperty("PaymentMethod", check.Item4),
                        new JProperty("FirstName", check.Item5),
                        new JProperty("LastName", check.Item6),
                        new JProperty("Email", check.Item7),
                        new JProperty("CheckInDate", check.Rest.Item1),
                        new JProperty("TransportationProviderName", check.Rest.Item2)
                    });
            }
            return pastTransportCheckIns;
        }
        public void PutTransportCheckInStatusBL(UserCheckIn userCheckIn)
        {
            transportDAL.PutTransportCheckInStatusDAL(userCheckIn);
        }

        public List<JObject> getUpcomingCheckIn(int transportproviderId)
        {
            var checkIn = transportDAL.getUpcomingCheckIn(transportproviderId);
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
                        new JProperty("TransportationProviderName", check.Rest.Item2)
                        }
                    );
            }

            return upComingCheckIn;

        }
    }
}
