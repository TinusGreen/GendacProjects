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

        public void updateContact(int id, string username, string lat, string lon)
        {
            var ctx = HttpContext.Current;
            var currentData = ((Contact[])ctx.Cache[CacheKey]).ToList();
            bool isFound = false;
            foreach (var x in currentData)
            {
                if (x.Id == id)
                {
                    //Found
                    x.Lat = lat;
                    x.Long = lon;
                    isFound = true;
                } 
                
            }

            if (isFound == false)
            {
                //New user
                Contact tempContact = new Contact();
                tempContact.Id = id;
                tempContact.Name = username;
                tempContact.Lat = lat;
                tempContact.Long = lon;
                currentData.Add(tempContact);
            }
            ctx.Cache[CacheKey] = currentData.ToArray();

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