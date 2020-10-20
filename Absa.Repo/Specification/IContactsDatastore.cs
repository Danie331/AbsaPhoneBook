using Absa.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Absa.Repo.Specification
{
    public interface IContactsDatastore
    {
        Task<Contact> AddAsync(Contact contact);
        Task<ContactDetail> AddAsync(int id, ContactDetail contactDetail);
        Task<IEnumerable<Contact>> FindContactsByNameAsync(string searchName, PagingFilter filter = null);
        Task<IEnumerable<Contact>> FindContactsByDetailAsync(string searchContactDetail, PagingFilter filter = null);
        Task<IEnumerable<Contact>> GetAllAsync(PagingFilter filter = null);
        Task<Contact> GetContactAsync(int id);
        Task<ContactDetail> GetContactDetailAsync(int id);
    }
}
