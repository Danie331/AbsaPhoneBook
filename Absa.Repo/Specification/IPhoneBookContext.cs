
using Absa.Repo.DbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace Absa.Repo.Specification
{
    public interface IPhoneBookContext
    {
        DbSet<Contact> Contact { get; set; }
        DbSet<ContactNumber> ContactNumber { get; set; }
        Microsoft.EntityFrameworkCore.DbContext Context { get; }
    }
}
