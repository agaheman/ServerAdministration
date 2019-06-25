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
using ServerAdministration.Server.Master.Services;

namespace ServerAdministration.Server.Master
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

            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"));
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IRepository<Insurance>, RepositoryMaster<Insurance>>();
            services.AddScoped<IRepository<IISLogEvent>, RepositorySlave<IISLogEvent>>();


        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<MasterDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection"),
                                        b => b.MigrationsAssembly("ServerAdministration.Server.Master"));

            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IAdministrationService, AdministrationService>();
            services.AddScoped<IRepository<Insurance>, RepositoryMaster<Insurance>>();
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

            app.UseCors();
            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }


    }
}
