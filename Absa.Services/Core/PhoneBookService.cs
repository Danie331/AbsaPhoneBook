﻿using Absa.Models;
using Absa.Repo.Specification;
using Absa.Services.Specification;
using System.Collections.Generic;
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

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            var newContact = await _store.AddAsync(contact);
            if (contact.ContactDetails != null)
            {
                foreach (var detail in contact.ContactDetails)
                {
                    await AddContactDetailAsync(newContact.Id, detail);
                }
            }

            return newContact;
        }

        public Task<ContactDetail> AddContactDetailAsync(int id, ContactDetail contactDetail) => _store.AddAsync(id, contactDetail);

        public Task<Contact> GetContactAsync(int id) => _store.GetContactAsync(id);

        public Task<ContactDetail> GetContactDetailAsync(int id) => _store.GetContactDetailAsync(id);

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

            return _store.GetAllAsync(filter);
        }
    }
}
