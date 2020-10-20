using AutoMapper;
using System.Linq;
using Domain = Absa.Models;
using Dto = Absa.API.DtoModels;

namespace Absa.API.Automapper
{
    public class DomainToApiProfile : Profile
    {
        public DomainToApiProfile()
        {
            CreateMap<Domain.Contact, Dto.Contact>().ForMember(c => c.ContactDetails, 
                                              f => f.MapFrom(s => string.Join(", ", s.ContactDetails.Select(e => e.PhoneNumber))));
            CreateMap<Domain.ContactDetail, Dto.ContactDetail>();
        }
    }
}
