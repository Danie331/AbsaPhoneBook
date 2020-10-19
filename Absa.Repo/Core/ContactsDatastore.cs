
using Absa.Models;
using Absa.Repo.DbContext;
using Absa.Repo.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data = Absa.Repo.DbContext.Models;

namespace Absa.Repo.Core
{
    public class ContactsDatastore : IContactsDatastore
    {
        private readonly IMapper _mapper;
        private Microsoft.Data.Sqlite.SqliteConnection _connection;

        public ContactsDatastore(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Contact> AddAsync(Contact contact)
        {
            try
            {
                var dto = _mapper.Map<Data.Contact>(contact);

                var ctx = await GetDataContext();

                ctx.Context.Entry(dto).State = EntityState.Added;

                await ctx.Context.SaveChangesAsync();

                return await GetContactAsync(dto.Id);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task<ContactDetail> AddAsync(int id, ContactDetail contactDetail)
        {
            try
            {
                var dto = _mapper.Map<Data.ContactNumber>(contactDetail);
                dto.ContactId = id;

                var ctx = await GetDataContext();

                ctx.Context.Entry(dto).State = EntityState.Added;

                await ctx.Context.SaveChangesAsync();

                return await GetContactDetailAsync(dto.Id);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> FindContactsByNameAsync(string searchName, PagingFilter filter = null)
        {
            try
            {
                IEnumerable<Data.Contact> results = null;
                var ctx = await GetDataContext();
                if (filter == null)
                {
                    results = await ctx.Contact.Where(c => c.FirstName.ToLower().Contains(searchName.ToLower()) || c.LastName.ToLower().Contains(searchName.ToLower()))
                                               .AsNoTracking().ToListAsync();
                }
                else
                {
                    results = await ctx.Contact.Where(c => c.FirstName.ToLower().Contains(searchName.ToLower()) || c.LastName.ToLower().Contains(searchName.ToLower()))
                                                             .Skip(filter.SkipLength)
                                                             .Take(filter.PageSize)
                                                             .AsNoTracking().ToListAsync();
                }

                return _mapper.Map<IEnumerable<Contact>>(results);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> FindContactsByDetailAsync(string searchContactDetail, PagingFilter filter = null)
        {
            try
            {
                var ctx = await GetDataContext();

                var results = await ctx.ContactNumber.Where(c => c.PhoneNumber.Contains(searchContactDetail)).AsNoTracking().ToListAsync();

                var contactIDs = results.Select(c => c.ContactId).ToList();
                var targetContacts = await ctx.Contact.Where(c => contactIDs.Contains(c.Id)).AsNoTracking().ToListAsync();

                return _mapper.Map<IEnumerable<Contact>>(targetContacts);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        private async Task<IPhoneBookContext> GetDataContext()
        {
            var connection = _connection ?? (_connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:"));
            connection.Open();
            var options = new DbContextOptionsBuilder<PhoneBookDirectoryContext>().UseSqlite(connection).Options;
            using (var context = new PhoneBookDirectoryContext(options))
            {
                await context.Database.EnsureCreatedAsync();
            }

            return new PhoneBookDirectoryContext(options);
        }

        private async Task<Contact> GetContactAsync(int id)
        {
            try
            {
                var ctx = await GetDataContext();

                var record = await ctx.Contact.AsNoTracking().FirstAsync(x => x.Id == id);

                return _mapper.Map<Contact>(record);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }

        private async Task<ContactDetail> GetContactDetailAsync(int id)
        {
            try
            {
                var ctx = await GetDataContext();

                var record = await ctx.ContactNumber.AsNoTracking().FirstAsync(x => x.Id == id);

                return _mapper.Map<ContactDetail>(record);
            }
            catch (Exception ex)
            {
                // Log ex
                throw;
            }
        }
    }
}
