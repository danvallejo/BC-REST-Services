﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HelloWorldService.Models;

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
        public Contact Get(int id)
        {
            return null;
        }

        // POST: api/Contacts
        public void Post([FromBody]Contact value)
        {
            contacts.Add(value); // This will add the contact to the list
        }

        // PUT: api/Contacts/5
        public void Put(int id, [FromBody]Contact value)
        {
        }

        // DELETE: api/Contacts/5
        public void Delete(int id)
        {
        }
    }
}
