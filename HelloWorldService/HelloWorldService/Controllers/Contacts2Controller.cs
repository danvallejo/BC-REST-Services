using HelloWorldService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HelloWorldService.Controllers
{
    [RoutePrefix("api/v2/contacts")]
    public class Contacts2Controller : ApiController
    {
        [Route]
        [HttpGet]
        public IEnumerable<Contact2> Get()
        {
            return new Contact2[] {
                new Contact2{Id=5, Name="steve", Age=21}
            };
        }
    }
}
