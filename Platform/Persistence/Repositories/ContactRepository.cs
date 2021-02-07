using Platform.GraphQL.Repositories;

using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;

namespace Platform.GraphQL.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly string _baseUrl;
        private RestSharp.RestClient client;

        public ContactRepository(string baseUrl)
        {
            _baseUrl = baseUrl;
            client =new  RestSharp.RestClient(new Uri(baseUrl));
        }

        public async Task<Contact.Projection.Models.Contact> FindByIdAsync(string id)
        {

            RestRequest request = new RestRequest($"Contact/{id}", Method.GET);
            IRestResponse<Contact.Projection.Models.Contact> response
                = client.Execute<Contact.Projection.Models.Contact>(request);
            return response.Data;
        }

        public async Task<IEnumerable<Contact.Projection.Models.Contact>> ListAsync()

        {
            RestRequest request = new RestRequest("Contact", Method.GET);
            IRestResponse<IEnumerable<Contact.Projection.Models.Contact>> response 
                = client.Execute<IEnumerable<Contact.Projection.Models.Contact>>(request);
            return response.Data;
        }
    }
}
