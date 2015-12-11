using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactManager.Models;



namespace ContactManager.Services
{
    public class ContactRepository
    {

        public ContactRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var contacts = new Contact[]
                    {
                new Contact
                {
                    Id = 1, Name = "Glenn Block", Long = "25.4", Lat = "45.7"
                },
                new Contact
                {
                    Id = 2, Name = "Dan Roth", Long = "35.2", Lat = "36.8"
                }
                    };

                    ctx.Cache[CacheKey] = contacts;
                }
            }
        }



        private const string CacheKey = "ContactStore";

        public bool SaveContact(Contact contact)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Contact[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(contact);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }



        public Contact[] GetAllContacts()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Contact[])ctx.Cache[CacheKey];
            }

            return new Contact[]
                {
            new Contact
            {
                Id = 0,
                Name = "Placeholder",
                Long = "0",
                Lat = "0"
            }
                };
        }

    }
}