using Newtonsoft.Json.Linq;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace PlanYourTrip_API.Controllers
{
    public class TransportController : ApiController
    {
        TransportBuisnessLogic transportBuisnessLogic = new TransportBuisnessLogic();

        // summary:This get method is used to fetch all the required details for the transport owner
        // It will help in displaying all the transports associated with that particular owner

        [HttpGet]
        [Route("api/Transport/{id}")]
        public IHttpActionResult GetAllTransportDetails(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please provide a not null id");
                }
                else
                {
                    return Ok(transportBuisnessLogic.getAllTransportDetails(id));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while getting the transportation details"));
            }
           
           
        }
        
        // summary:This get method is used to fetch the checkin details of a particular transport for that particular date 

        [HttpGet]
        [Route("api/Transport/CheckinsT/{id}")]
        public IHttpActionResult GetTodaysCheckIn(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please provide a not null id");
                }
                else
                {
                    return Ok(transportBuisnessLogic.getTodaysCheckIn(id));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while getting the transportation details"));
            }
        }

        // summary:This get method is used to fetch the checkin details of a particular transport for past dates 

        [HttpGet]
        [Route("api/Transport/CheckinsP/{id}")]
        public IHttpActionResult GetPastCheckIn(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please provide a not null id");
                }
                else
                {
                    return Ok(transportBuisnessLogic.getPastCheckIn(id));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while getting the transportation details"));
            }
        }

        // summary:This get method is used to fetch the checkin details of a particular transport for future dates

        [HttpGet]
        [Route("api/Transport/CheckinsF/{id}")]
        public IHttpActionResult GetUpcomingCheckIn(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Please provide a not null id");
                }
                else
                {
                    return Ok(transportBuisnessLogic.getUpcomingCheckIn(id));
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while getting the transportation details"));
            }
        }

        // summary:This put method is used to take the checkin status of a particular transport on the present date
        // this is updated when the transport owner himself checks in the user after user started to avail that particular transport
        [HttpPut]
        [Route("api/transportcheckin")]
        public IHttpActionResult PutTransportCheckInStatus(UserCheckIn userCheckIn)
        {
            try
            {
                if (userCheckIn == null)
                {
                    return BadRequest("Please provide valid data for updating the check-in status of the user");
                }
                else
                {
                   transportBuisnessLogic.PutTransportCheckInStatusBL(userCheckIn);
                    return Ok();
                }
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while getting the transportation details"));

            }


        }
    }
}
