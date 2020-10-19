
using Absa.Services.Core;
using Absa.Services.Specification;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Absa.Services
{
    public static class DependencyMapper
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPhoneBookService, PhoneBookService>();

            Repo.DependencyMapper.RegisterRepo(services, configuration);
        }
    }
}
