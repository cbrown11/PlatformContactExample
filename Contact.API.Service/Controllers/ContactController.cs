using Contact.Service.Command;
using Contact.Service.Extensions;
using DomainBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        protected IDomainRepository _domainRepository;

        public ContactController(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        // POST api/<ContactController>
        [HttpPost]
        public void Post([FromBody] CreateContact createContact)
        {

            var contact = Domain.Contact.Create(createContact.AuditInfo,createContact.UserId, 
                createContact.Name,createContact.DateOfBirth, createContact.EmailAddress, createContact.Address,
                DateTime.UtcNow);
            _domainRepository.Save(contact);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateContact createContact)
        {
            // try
            // https://www.freecodecamp.org/news/an-awesome-guide-on-how-to-build-restful-apis-with-asp-net-core-87b818123e28/
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var contact = Domain.Contact.Create(createContact.AuditInfo, createContact.UserId,
                           createContact.Name, createContact.DateOfBirth, createContact.EmailAddress, createContact.Address,
                           DateTime.UtcNow);
            _domainRepository.Save(contact);;
            return Ok(contact);
        }

    }
}
