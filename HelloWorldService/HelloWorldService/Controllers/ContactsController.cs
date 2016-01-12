﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloWorldService.Models;
using Newtonsoft.Json;

namespace HelloWorldService.Controllers
{
    public class ContactsController : ApiController
    {
        public static List<Contact> contacts = new List<Contact>();

        // GET: api/Contacts
        public IEnumerable<Contact> Get()
        {
            return contacts;
        }

        // GET: api/Contacts/5
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
