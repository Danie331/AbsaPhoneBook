
using Absa.Models;
using Absa.Models.Exceptions;
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
                throw new DatastoreException("Exception in data access layer", ex);
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
                throw new DatastoreException("Exception in data access layer", ex);
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
                                               .Include(c => c.ContactNumber)
                                               .AsNoTracking().ToListAsync();
                }
                else
                {
                    results = await ctx.Contact.Where(c => c.FirstName.ToLower().Contains(searchName.ToLower()) || c.LastName.ToLower().Contains(searchName.ToLower()))
                                               .Include(c => c.ContactNumber)
                                               .Skip(filter.SkipLength)
                                               .Take(filter.PageSize)
                                               .AsNoTracking().ToListAsync();
                }

                return _mapper.Map<IEnumerable<Contact>>(results);
            }
            catch (Exception ex)
            {
                // Log ex
                throw new DatastoreException("Exception in data access layer", ex);
            }
        }

        public async Task<IEnumerable<Contact>> FindContactsByDetailAsync(string searchContactDetail, PagingFilter filter = null)
        {
            try
            {
                var ctx = await GetDataContext();

                var results = await ctx.ContactNumber.Where(c => c.PhoneNumber.Contains(searchContactDetail))
                                                     .Include(c => c.Contact)
                                                     .AsNoTracking().ToListAsync();

                return _mapper.Map<IEnumerable<Contact>>(results.Select(c => c.Contact));
            }
            catch (Exception ex)
            {
                // Log ex
                throw new DatastoreException("Exception in data access layer", ex);
            }
        }

        public async Task<IEnumerable<Contact>> GetAllAsync(PagingFilter filter = null)
        {
            try
            {
                var ctx = await GetDataContext();
                IEnumerable<Data.Contact> records = null;
                if (filter == null)
                {
                    records = await ctx.Contact.Include(c => c.ContactNumber)
                                               .AsNoTracking().ToListAsync();
                }
                else
                {
                    records = await ctx.Contact.Include(c => c.ContactNumber)
                                               .Skip(filter.SkipLength)
                                               .Take(filter.PageSize)
                                               .AsNoTracking().ToListAsync();
                }

                return _mapper.Map<IEnumerable<Contact>>(records);
            }
            catch (Exception ex)
            {
                // Log ex
                throw new DatastoreException("Exception in data access layer", ex);
            }
        }

        private async Task<IPhoneBookContext> GetDataContext()
        {
            var connection = _connection ?? (_connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource='file::memory:?cache=shared'"));
            connection.Open();
            var options = new DbContextOptionsBuilder<PhoneBookDirectoryContext>().UseSqlite(connection).Options;
            using (var context = new PhoneBookDirectoryContext(options))
            {
                await context.Database.EnsureCreatedAsync();
            }

            return new PhoneBookDirectoryContext(options);
        }

        public async Task<Contact> GetContactAsync(int id)
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
                throw new DatastoreException("Exception in data access layer", ex);
            }
        }

        public async Task<ContactDetail> GetContactDetailAsync(int id)
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
                throw new DatastoreException("Exception in data access layer", ex);
            }
        }
    }
}
