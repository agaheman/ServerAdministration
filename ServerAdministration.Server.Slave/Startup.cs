using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.DataAccess.DbContexts;
using ServerAdministration.Server.DataAccess.Repositories;
using ServerAdministration.Server.Entities;
using ServerAdministration.Server.Slave.Services;

namespace ServerAdministration.Server.Slave
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {


            services.AddDbContext<SlaveDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"));

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISiteInfoService, SiteInfoService>();
            services.AddScoped< IRepository<IISLogEvent>,Repository<IISLogEvent>>();


        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<SlaveDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISiteInfoService, SiteInfoService>();
            services.AddScoped<IRepository<SiteIISLog>, Repository<SiteIISLog>>();
            services.AddScoped<IRepository<IISLogEvent>, Repository<IISLogEvent>>();

        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}
