using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloWorldService.Models;
using Newtonsoft.Json;

namespace HelloWorldService.Controllers
{
    /// <summary>
    /// This is the Contacts API
    /// </summary>
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
   // [Authenticator]
    [RoutePrefix("api/contacts")]
    public class ContactsController : ApiController
    {
        public static List<Contact> contacts = new List<Contact>();


        // GET: api/Contacts
        /// <summary>
        /// This is the get function
        /// </summary>
        /// <returns></returns>
        [Route]
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            //int x = 1;
            //x = x / (x - 1);
            return contacts;
        }

        // GET: api/Contacts/5
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var contact = contacts.SingleOrDefault(t => t.Id == id);
            if (contact == null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var newJson = JsonConvert.SerializeObject(contact);

            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = postContent
            };
        }

        static int nextId = 101;

        // POST: api/Contacts
        [Route]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Contact value)
        {
            if (value == null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }

            value.Id = nextId++;
            contacts.Add(value);

            var result = new { Id = value.Id, HasCandy = true };

            var newJson = JsonConvert.SerializeObject(result);

            var postContent = new StringContent(newJson, System.Text.Encoding.UTF8, "application/json");

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Created,
                Content = postContent
            };
        }

        // PUT: api/Contacts/5
        [HttpPut]
        [Route("{id}")]
        public void Put(int id, [FromBody]Contact value)
        {
            var contact = contacts.SingleOrDefault(t => t.Id == id);

            if (contact != null)
            {
                contact.Name = value.Name;
                contact.Phones = value.Phones;
                contact.DateAdded = value.DateAdded;
            }
        }

        // DELETE: api/Contacts/5
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var contact = contacts.SingleOrDefault(t => t.Id == id);
            if (contact == null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            contacts.RemoveAll(t => t.Id == id);

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
