using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileCRM.Services;
using MobileCRM.Models;

[assembly:Dependency(typeof(ContactRepository))]

namespace MobileCRM.Services
{
    public class ContactRepository : InMemoryRepository<Contact>
    {
        public ContactRepository()
        {
            AddRange(new List<Contact>
                {
                    new Contact
                    { 
                        FirstName = "Tinus", 
                        LastName = "Green", 
                        Industry = "Software", 
                        Address = new Address
                        {
                            Latitude = -25.7457,
                            Longitude = 028.2734
                        } 
                    },
                    new Contact
                    { 
                        FirstName = "Dory", 
                        LastName = "Himenez", 
                        Industry = "Logistic", 
                        Address = new Address
                        {
                            Latitude = -25.7457,
                            Longitude = 028.2735
                        } 
                    },
                    new Contact
                    { 
                        FirstName = "Aria", 
                        LastName = "Patel", 
                        Industry = "Aerospace", 
                        Address = new Address
                        {
                            Latitude = -25.7458,
                            Longitude = 028.2734
                        } 
                    }
                });
        }
    }
}