
using Absa.Models.Exceptions;
using Absa.Services.Specification;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Absa.Tests
{
    [TestFixture]
    public class PhoneBookTests
    {
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            _serviceProvider = new DependencyMapper().CreateServiceProvider();
        }

        [Test]
        public async Task Test_Add_Contact()
        {
            var service = _serviceProvider.GetService<IPhoneBookService>();

            await service.AddContactAsync(new Models.Contact { Id = 100, FirstName = "Bugs", LastName = "Bunny" });

            var testAddContact = await service.GetContactAsync(100);

            Assert.IsTrue(testAddContact.Id == 100 && testAddContact.FirstName.Equals("Bugs") && testAddContact.LastName.Equals("Bunny"));
        }

        [Test]
        public async Task Test_Add_Duplicate_Key_Fails()
        {
            // Assumption: database is pre-seeded with a contact with ID = 1

            var service = _serviceProvider.GetService<IPhoneBookService>();

            // Verify pre-seeded value
            Assert.IsNotNull(await service.GetContactAsync(1));

            Assert.ThrowsAsync<DatastoreException>(async () => await service.AddContactAsync(new Models.Contact { Id = 1, FirstName = "Bugs", LastName = "Bunny" }));
        }

        [Test]
        public async Task Test_Add_Contact_Detail()
        {
            // Assumption: database is pre-seeded with a contact with ID = 1

            var service = _serviceProvider.GetService<IPhoneBookService>();

            var cd = await service.AddContactDetailAsync(1, new Models.ContactDetail { ContactId = 1, PhoneNumber = "123456789" });

            var testDetail = await service.GetContactDetailAsync(cd.Id);

            Assert.IsTrue(cd.Id == testDetail.Id && testDetail.ContactId == 1 && cd.PhoneNumber == testDetail.PhoneNumber && cd.PhoneNumber == "123456789");
        }

        [Test]
        public async Task Test_Find_Contact_By_Name()
        {
            var service = _serviceProvider.GetService<IPhoneBookService>();

            await service.AddContactAsync(new Models.Contact { Id = 100, FirstName = "Bugs", LastName = "Bunny" });

            var search = await service.SearchContactsAsync(new Models.ContactSearchData { SearchName = "Bugs" });

            Assert.IsTrue(search.Any(c => c.Id == 100 && c.FirstName == "Bugs"));
        }

        [Test]
        public async Task Test_Find_Contact_By_PhoneNumber()
        {
            var service = _serviceProvider.GetService<IPhoneBookService>();

            await service.AddContactDetailAsync(1, new Models.ContactDetail { ContactId = 1, PhoneNumber = "123456789" });

            var search = await service.SearchContactsAsync(new Models.ContactSearchData { SearchNumber = "123456789" });

            Assert.IsTrue(search.Any(c => c.Id == 1 && c.ContactDetails.Any(b => b.PhoneNumber == "123456789")));
        }
    }
}
