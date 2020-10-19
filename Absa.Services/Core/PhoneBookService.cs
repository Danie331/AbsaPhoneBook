using Absa.Models;
using Absa.Repo.Specification;
using Absa.Services.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Absa.Services.Core
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IContactsDatastore _store;
        public PhoneBookService(IContactsDatastore store)
        {
            _store = store;
        }

        public Task<Contact> AddContactAsync(Contact contact) => _store.AddAsync(contact);

        public Task<ContactDetail> AddContactDetailAsync(int id, ContactDetail contactDetail) => _store.AddAsync(id, contactDetail);

        public Task<IEnumerable<Contact>> SearchContactsAsync(ContactSearchData searchData, PagingFilter filter = null)
        {
            if (!string.IsNullOrWhiteSpace(searchData.SearchName))
            {
                return _store.FindContactsByNameAsync(searchData.SearchName, filter);
            }

            if (!string.IsNullOrWhiteSpace(searchData.SearchNumber))
            {
                return _store.FindContactsByDetailAsync(searchData.SearchNumber, filter);
            }

            return Task.FromResult(Enumerable.Empty<Contact>());
        }
    }
}
