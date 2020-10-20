
using Absa.Repo.Core;
using Absa.Repo.DbContext;
using Absa.Repo.Specification;
using Absa.Services.Core;
using Absa.Services.Specification;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Absa.Tests
{
    public class DependencyMapper
    {
        public IServiceProvider CreateServiceProvider()
        {
            var serviceProviderCollection = new ServiceCollection()
                                           .AddScoped<IContactsDatastore, ContactsDatastore>()
                                           .AddScoped<PhoneBookDirectoryContext, PhoneBookDirectoryContext>()
                                           .AddScoped<IPhoneBookService, PhoneBookService>();
            serviceProviderCollection.AddAutoMapper(GetType().Assembly, typeof(Repo.DependencyMapper).Assembly);

            return serviceProviderCollection.BuildServiceProvider();
        }
    }
}
