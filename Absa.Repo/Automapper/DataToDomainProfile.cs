
using AutoMapper;
using Domain = Absa.Models;
using Data = Absa.Repo.DbContext.Models;

namespace Absa.Repo.Automapper
{
    public class DataToDomainProfile : Profile
    {
        public DataToDomainProfile()
        {
            CreateMap<Data.ContactNumber, Domain.ContactDetail>();
            CreateMap<Data.Contact, Domain.Contact>().ForMember(x => x.ContactDetails, q => q.MapFrom(r => r.ContactNumber));
            CreateMap<Data.Contact, Domain.Contact>().ForMember(x => x.ContactDetails, q => q.MapFrom(r => r.ContactNumber));
        }
    }
}
