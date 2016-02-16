using HelloWorldService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorldService
{
    public interface IContactRepository
    {
        IEnumerable<Models.ModelContact> Contacts { get; }

        Models.ModelContact GetById(int id);

        int Add(Models.ModelContact value);

        void UpdateById(int id, Models.ModelContact value);

        bool DeleteById(int id);
    }

    public class ContactRepository : IContactRepository
    {
        private ContactsEntities db;

        public ContactRepository()
        {
            db = new ContactsEntities();
            db.Database.Connection.Open();
        }

        public IEnumerable<Models.ModelContact> Contacts
        {
            get
            {
                var contacts = new List<Models.ModelContact>();

                foreach (var contact in db.Contacts)
                {
                    var modelContact = new Models.ModelContact
                    {
                        Id = contact.Id,
                        Name = contact.Name,
                        DateAdded = contact.DateAdded
                    };

                    var modelPhones = new List<Models.ModelPhone>();
                    foreach (var phone in contact.ContactPhones)
                    {
                        modelPhones.Add(new Models.ModelPhone 
                        {
                            Number = phone.PhoneNumber,
                            PhoneType = (Models.ModelPhoneType)Enum.Parse(typeof(Models.ModelPhoneType), phone.PhoneType)
                        });
                    }

                    if (modelPhones.Any())
                    {
                        modelContact.Phones = modelPhones.ToArray();
                    }

                    contacts.Add(modelContact);
                }

                return contacts;
            }
        }

        public Models.ModelContact GetById(int id)
        {
            var entity = db.Contacts.SingleOrDefault(t => t.Id == id);

            if (entity == null)
            {
                return null;
            }

            return new Models.ModelContact
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    DateAdded = entity.DateAdded
                };
        }

        public int Add(Models.ModelContact value)
        {
            var entity = new Contact
            {
                Id = value.Id,
                Name = value.Name,
                DateAdded = DateTime.UtcNow
            };

            if (value.Phones != null)
            {
                foreach (var phone in value.Phones)
                {
                    entity.ContactPhones.Add(new ContactPhone
                    {
                        PhoneNumber = phone.Number,
                        PhoneType = phone.PhoneType.ToString()
                    });
                }
            }

            db.Contacts.Add(entity);

            db.SaveChanges();

            return entity.Id;
        }

        public void UpdateById(int id, Models.ModelContact value)
        {
            var entity = db.Contacts.SingleOrDefault(t => t.Id == id);

            if (entity != null)
            {
                entity.Name = value.Name;
                //contact.Phones = value.Phones;

                db.SaveChanges();
            }
        }

        public bool DeleteById(int id)
        {
            var entity = db.Contacts.SingleOrDefault(t => t.Id == id);
            if (entity == null)
            {
                return false;
            }

            db.Contacts.Remove(entity);
            db.SaveChanges();

            return true;
        }
    }
}