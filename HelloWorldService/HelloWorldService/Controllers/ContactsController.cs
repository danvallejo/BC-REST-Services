﻿using System.Collections.Generic;
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
        private IContactRepository contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        // GET: api/Contacts
        /// <summary>
        /// This is the get function
        /// </summary>
        /// <returns></returns>
        [Route]
        [HttpGet]
        public IEnumerable<Models.ModelContact> Get()
        {
            //int x = 1;
            //x = x / (x - 1);
            return contactRepository.Contacts;
        }

        // GET: api/Contacts/5
        [Route("{id}")]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var contact = contactRepository.GetById(id);
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

       

        // POST: api/Contacts
        [Route]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Models.ModelContact value)
        {
            if (value == null)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                };
            }
          
            var contactId = contactRepository.Add(value);

            var result = new { Id = contactId, HasCandy = true };

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
        public void Put(int id, [FromBody]Models.ModelContact value)
        {
            contactRepository.UpdateById(id, value);
        }

        // DELETE: api/Contacts/5
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var deleted = contactRepository.DeleteById(id);

            if (!deleted)
            {
                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
