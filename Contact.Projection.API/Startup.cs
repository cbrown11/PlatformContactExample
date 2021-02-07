using Contact.Projection.Persistence.Repositories;
using Contact.Projection.Repositories;
using Contact.Projection.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Contact.Projection.API.Extensions;

namespace Contact.Projection.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomSwagger();
            services.AddSingleton<IContactService, ContactService>();
            services.AddSingleton<IContactRepository, ContactRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();
        }

 

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
         
            app.UseHttpsRedirection();
            app.UseCustomSwagger();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
