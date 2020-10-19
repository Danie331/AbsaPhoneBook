
using Absa.Repo.Core;
using Absa.Repo.DbContext;
using Absa.Repo.Specification;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Absa.Repo
{
    public static class DependencyMapper
    {
        public static void RegisterRepo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContactsDatastore, ContactsDatastore>();

            services.AddDbContext<IPhoneBookContext, PhoneBookDirectoryContext>(options =>
            {
                var connection = new SqliteConnection("DataSource='file::memory:?cache=shared'");
                connection.Open();
                options.UseSqlite(connection);
            });
        }
    }
}
