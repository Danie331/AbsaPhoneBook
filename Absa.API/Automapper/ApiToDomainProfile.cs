using AutoMapper;
using Domain = Absa.Models;
using Dto = Absa.API.DtoModels;

namespace Absa.API.Automapper
{
    public class ApiToDomainProfile : Profile
    {
        public ApiToDomainProfile()
        {
            CreateMap<Dto.PagedQuery, Domain.PagingFilter>();
            CreateMap<Dto.Contact, Domain.Contact>();
            CreateMap<Dto.ContactDetail, Domain.ContactDetail>().ForMember(i => i.ContactId, f => f.Ignore());
            CreateMap<Dto.SearchData, Domain.ContactSearchData>();
        }
    }
}
