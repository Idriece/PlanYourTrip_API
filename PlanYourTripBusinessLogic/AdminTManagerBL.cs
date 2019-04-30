using PlanYourTripBusinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanYourTripDataAccessLayer;
using Newtonsoft.Json.Linq;

namespace PlanYourTripBusinessLogic
{
    public class AdminTManagerBL
    {
        readonly AdminTManagerDAL adminTManagerDAL = new AdminTManagerDAL();

        //This method is invoked on addition of new transportation providers
        //It's returning id to method which is calling it, it get's id from DAL which is returning the max Id as
        //it points to the newly inserted records.
        //this id is required for creation of price entries in transportationPrice table
        public int AddTProviderBL(TransportationProvider transportationProvider)
        {
            return adminTManagerDAL.AddTProviderDAL(transportationProvider);
        }

        public List<JObject> GetAllTransportCityListBL()
        {
            return adminTManagerDAL.GetAllTransportCityListDAL().Select(
                x => new JObject
                {
                    new JProperty("TransportationProviderName", x.Item1),
                    new JProperty("CityName", x.Item2)
                }).ToList();
        }

        public List<JObject> GetOwnerList()
        {
            var ownerList = adminTManagerDAL.GetOwnerList();
            var transformedOwnerList = new List<JObject>();

            foreach (var owner in ownerList)
            {
                var ownerTransform = new JObject(
                    new JProperty("Id", owner.Item1),
                    new JProperty("Email", owner.Item2)
                    );
                transformedOwnerList.Add(ownerTransform);
            }

            return transformedOwnerList;
        }

        //This mehtod is invoked when the Associated Tprovider get's successfully registered.
        //and it makes entries for price. since it takes list argument of price type it iterates through
        //the list and call the AddTProviderPriceDAL method of DAL to add new price entries.
        public void AddTProviderPriceBL(List<TransportationPrice> transportationPrices)
        {
            foreach(TransportationPrice price in transportationPrices)
            {
                adminTManagerDAL.AddTProviderPriceDAL(price);
            }
        }
        public List<JObject> GetAllTProviderDetailsBL()
        {
            var allTProviderDetails = adminTManagerDAL.GetAllTProviderDetailsDAL();

            var groupedTransportationProvider = allTProviderDetails
                                                .GroupBy(x => new { x.Item1, x.Item2, x.Item3, x.Item4}).ToList();

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

        public void UpdateTProviderBL(TransportationProvider transportationProvider)
        {
            adminTManagerDAL.UpdateTProviderDAL(transportationProvider);
        }

        public void UpdateTProviderPrices(List<TransportationPrice> transportationPrices)
        {
            foreach(var price in transportationPrices)
            {
                adminTManagerDAL.UpdateTProviderPriceDAL(price);
            }
        }

        public bool AddNewPrice(TransportationPrice transportationPrice)
        {
            return adminTManagerDAL.AddNewPriceDAL(transportationPrice);
        }
    }
}
