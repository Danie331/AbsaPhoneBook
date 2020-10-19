using AutoMapper;
using Domain = Absa.Models;
using Dto = Absa.API.DtoModels;

namespace Absa.API.Automapper
{
    public class DomainToApiProfile : Profile
    {
        public DomainToApiProfile()
        {
            CreateMap<Domain.Contact, Dto.Contact>();
            CreateMap<Domain.ContactDetail, Dto.ContactDetail>();
        }
    }
}
