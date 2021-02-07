using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contact.Projection.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Contact.Projection.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private readonly IContactService _contactService;
 

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        // GET: api/<ContactController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Models.Contact>), 200)]
        public async Task<IEnumerable<Models.Contact>> Get()
        {
            var contact = await _contactService.ListAsync();
            return contact;
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Models.Contact>), 200)]
        public async Task<Models.Contact> Get(string id)
        {
            var contacts = await _contactService.GetByIdAsync(id);
            return contacts;
        }
    }
}
