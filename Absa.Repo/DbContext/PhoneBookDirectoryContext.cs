using Absa.Repo.DbContext.Models;
using Absa.Repo.Specification;
using Microsoft.EntityFrameworkCore;

namespace Absa.Repo.DbContext
{
    public class PhoneBookDirectoryContext : Microsoft.EntityFrameworkCore.DbContext, IPhoneBookContext
    {
        public PhoneBookDirectoryContext()
        {
        }

        public PhoneBookDirectoryContext(DbContextOptions<PhoneBookDirectoryContext> options)
            : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbContext Context => this;

        public DbSet<Contact> Contact { get; set; }
        public DbSet<ContactNumber> ContactNumber { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<ContactNumber>().ToTable("ContactNumber");

            // Seed data
            modelBuilder.Entity<Contact>().HasData(
                        new Contact { Id = 1, FirstName = "Danie", LastName = "vd M" },
                        new Contact { Id = 2, FirstName = "Donald", LastName = "Duck" },
                        new Contact { Id = 3, FirstName = "Mickey", LastName = "Mouse" });
            modelBuilder.Entity<ContactNumber>().HasData(
                        new ContactNumber { Id = 1, ContactId = 1, PhoneNumber = "0724707471" },
                        new ContactNumber { Id = 2, ContactId = 1, PhoneNumber = "0769561578" },
                        new ContactNumber { Id = 3, ContactId = 3, PhoneNumber = "0848009229" },
                        new ContactNumber { Id = 4, ContactId = 2, PhoneNumber = "0846569228" },
                        new ContactNumber { Id = 5, ContactId = 3, PhoneNumber = "+27848009229" });
        }
    }
}
