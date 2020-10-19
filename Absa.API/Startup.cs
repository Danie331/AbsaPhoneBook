using Absa.API.Middleware;
using Absa.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Absa.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(_configuration);

            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddAutoMapper(GetType().Assembly, typeof(Repo.DependencyMapper).Assembly);

            services.AddSwaggerDocument(settings => settings.Title = "Absa Phonebook Assessment");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
