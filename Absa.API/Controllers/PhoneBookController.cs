using Absa.Services.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Domain = Absa.Models;
using Dto = Absa.API.DtoModels;

namespace Absa.API.Controllers
{
    [AllowAnonymous, Route("contacts"), ApiController, ApiVersion("1.0")]
    public class PhoneBookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPhoneBookService _phoneBookService;

        public PhoneBookController(IPhoneBookService phoneBookService,
                                   IMapper mapper)
        {
            _phoneBookService = phoneBookService;
            _mapper = mapper;
        }

        [HttpPost, Consumes(MediaTypeNames.Application.Json), ProducesResponseType(201)]
        public async Task<IActionResult> AddContact(Dto.Contact contactDto)
        {
            var contact = _mapper.Map<Domain.Contact>(contactDto);
            var result = await _phoneBookService.AddContactAsync(contact);

            return Ok(new Dto.Response<Dto.Contact>(_mapper.Map<Dto.Contact>(result)));
        }

        [HttpPost, Route("{id}"), Consumes(MediaTypeNames.Application.Json), ProducesResponseType(201)]
        public async Task<IActionResult> AddContactDetail(int id, [FromBody]Dto.ContactDetail contactDetailDto)
        {
            var contactDetail = _mapper.Map<Domain.ContactDetail>(contactDetailDto);
            var result = await _phoneBookService.AddContactDetailAsync(id, contactDetail);

            return Ok(new Dto.Response<Dto.ContactDetail>(_mapper.Map<Dto.ContactDetail>(result)));
        }

        [HttpGet]
        public async Task<IActionResult> SearchContacts([FromQuery] Dto.PagedQuery paging, [FromQuery] Dto.SearchData dataDto)
        {
            var pagingFilter = _mapper.Map<Domain.PagingFilter>(paging);
            var data = _mapper.Map<Domain.ContactSearchData>(dataDto);

            var results = await _phoneBookService.SearchContactsAsync(data, pagingFilter);
            var resultsDto = _mapper.Map<IEnumerable<Dto.Contact>>(results);

            return Ok(new Dto.PagedResponse<Dto.Contact>(resultsDto, pagingFilter.PageNumber, pagingFilter.PageSize));
        }
    }
}
