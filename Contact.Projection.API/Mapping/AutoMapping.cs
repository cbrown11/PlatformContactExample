// AutoMapping.cs
using AutoMapper;


namespace Contact.Projection.API.Mapping
{

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Contact.Domain.Events.ContactCreated, Models.Contact>()
                .AfterMap((src, dest)=> {
                    dest.FirstName = src.Name.FirstName;
                    dest.LastName = src.Name.LastName;
                });
            CreateMap<Contact.Domain.ValueObjects.Address, Models.Address>();
        }
    }
}
