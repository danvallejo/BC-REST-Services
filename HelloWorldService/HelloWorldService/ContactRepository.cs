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
                return db.Contacts.Select(t =>
                    new Models.ModelContact
                {
                    Id = t.Id,
                    Name = t.Name,
                    DateAdded = t.DateAdded
                });
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