using HelloWorldService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorldService
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Contacts { get; }

        Contact GetById(int id);

        void Add(Contact value);

        void UpdateById(int id, Contact value);

        bool DeleteById(int id);
    }

    public class ContactRepository : IContactRepository
    {
        private static int nextId = 101;
        private static List<Contact> contacts = new List<Contact>();

        public IEnumerable<Contact> Contacts
        {
            get
            {
                return contacts;
            }
        }

        public Contact GetById(int id)
        {
            return contacts.SingleOrDefault(t => t.Id == id);
        }

        public void Add(Contact value)
        {
            value.Id = nextId++;
            contacts.Add(value);
        }
        
        public void UpdateById(int id, Contact value)
        {
            var contact = GetById(id);

            if (contact != null)
            {
                contact.Name = value.Name;
                contact.Phones = value.Phones;
                contact.DateAdded = value.DateAdded;
            }
        }

        public bool DeleteById(int id)
        {
            var itemsRemoved = contacts.RemoveAll(t => t.Id == id);

            return (itemsRemoved > 0);
        }
    }
}