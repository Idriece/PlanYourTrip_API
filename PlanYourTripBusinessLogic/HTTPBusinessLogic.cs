using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessLogic
{
    public static class HTTPBusinessLogic
    {
        public static HttpResponseMessage SetHttpResponse(HttpStatusCode StatusCode, object ResponseMessage)
        {
            return new HttpResponseMessage
            {
                Content = new ObjectContent<JObject>(new JObject
                {
                    new JProperty("message", ResponseMessage)
                }, new JsonMediaTypeFormatter()),
                StatusCode = StatusCode
            };
        }
    }
}
