
using AutoMapper;
using Domain = Absa.Models;
using Data = Absa.Repo.DbContext.Models;

namespace Absa.Repo.Automapper
{
    public class DomainToDataProfile : Profile
    {
        public DomainToDataProfile()
        {
            CreateMap<Domain.ContactDetail, Data.ContactNumber>();
            CreateMap<Domain.Contact, Data.Contact>().ForMember(c => c.ContactNumber, f => f.Ignore());            
        }
    }
}
