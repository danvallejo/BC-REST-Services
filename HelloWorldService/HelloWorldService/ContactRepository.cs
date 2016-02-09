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
    }

    public class ContactRepository : IContactRepository
    {
        public IEnumerable<Contact> Contacts
        {
            get
            {
                var items = new[]
                {
                    new Contact{ Name = "First One"},
                    new Contact{ Name="Second One"},
                    new Contact{ Name="Third One"} ,
                    new Contact{ Name="Fourth One"},
                };
                return items;
            }
        }
    }
}