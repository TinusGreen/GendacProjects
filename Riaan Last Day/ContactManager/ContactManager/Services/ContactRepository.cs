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
                    Id = 0, Name = "Database", Long = "0", Lat = "0"
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
            if (username != null)
            {

                foreach (var x in currentData)
                {
                    if (x.Name == username)
                    {
                        //Found
                        x.Lat = lat.Replace(",",".");
                        x.Long = lon.Replace(",", ".");
                        isFound = true;
                    }

                }

                if (isFound == false)
                {
                    //New user
                    Contact tempContact = new Contact();
                    tempContact.Id = 0;
                    tempContact.Name = username;
                    tempContact.Lat = lat.Replace(",", ".");
                    tempContact.Long = lon.Replace(",", ".");
                    currentData.Add(tempContact);
                }
                ctx.Cache[CacheKey] = currentData.ToArray();
            }
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