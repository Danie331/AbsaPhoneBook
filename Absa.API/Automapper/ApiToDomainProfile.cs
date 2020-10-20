using AutoMapper;
using System.Linq;
using Domain = Absa.Models;
using Dto = Absa.API.DtoModels;

namespace Absa.API.Automapper
{
    public class ApiToDomainProfile : Profile
    {
        public ApiToDomainProfile()
        {
            CreateMap<Dto.PagedQuery, Domain.PagingFilter>();
            CreateMap<Dto.Contact, Domain.Contact>().ForMember(v => v.ContactDetails, f => f.MapFrom(q => q.ContactDetails
                                                                                                    .Split(new[] { ',' })
                                                                                                    .Select(n => new Domain.ContactDetail { ContactId = q.Id, PhoneNumber = n })));
            CreateMap<Dto.ContactDetail, Domain.ContactDetail>().ForMember(i => i.ContactId, f => f.Ignore());
            CreateMap<Dto.SearchData, Domain.ContactSearchData>();
        }
    }
}
