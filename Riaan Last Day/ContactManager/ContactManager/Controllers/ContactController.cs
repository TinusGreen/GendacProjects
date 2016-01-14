using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactManager.Services;


namespace ContactManager.Controllers
{
    public class ContactController : ApiController
    {

        private ContactRepository contactRepository;

        public ContactController()
        {
            this.contactRepository = new ContactRepository();
        }

        public Contact[] Get()
        {
            return contactRepository.GetAllContacts();
        }

        public IHttpActionResult GetProduct(int id, string user, string lat, string lon)
        {
            var contact = contactRepository.GetAllContacts();
            contactRepository.updateContact(id, user, lat, lon);
            var product = contact.FirstOrDefault((p) => p.Name == user);
            if (product == null)
            {
                return Ok(product);
            }
            return Ok(product);
        }

        public HttpResponseMessage Post(Contact contact)
        {
            this.contactRepository.SaveContact(contact);

            var response = Request.CreateResponse<Contact>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }


    }
}
