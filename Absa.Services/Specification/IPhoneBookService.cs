
using Absa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absa.Services.Specification
{
    public interface IPhoneBookService
    {
        Task<Contact> AddContactAsync(Contact contact);
        Task<ContactDetail> AddContactDetailAsync(int id, ContactDetail contactDetail);
        Task<IEnumerable<Contact>> SearchContactsAsync(ContactSearchData searchData, PagingFilter filter = null);
        Task<Contact> GetContactAsync(int id);
        Task<ContactDetail> GetContactDetailAsync(int id);
    }
}