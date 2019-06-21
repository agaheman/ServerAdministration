using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerAdministration.Server.DataAccess.Configurations;
using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.DataAccess.DbContexts;
using ServerAdministration.Server.DataAccess.Repositories;
using ServerAdministration.Server.Entities;
using ServerAdministration.Server.Slave.Extensions;
using ServerAdministration.Server.Slave.Services;
using Microsoft.Extensions.Options;


namespace ServerAdministration.Server.Slave
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //ServiceBase.Run(new ServiceBase[] { new FolderWatcher() });

            services.AddDbContext<SlaveDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection"),
                                                            b => b.MigrationsAssembly("ServerAdministration.Server.Slave"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddOptions();
            services.ConfigureWritable<ServerConfiguration>(Configuration.GetSection("ServerConfiguration"), "appsettings.json");


            services.AddScoped<ISiteInfoService, SiteInfoService>();
            services.AddScoped<IRepository<IISLogEvent>, RepositorySlave<IISLogEvent>>();


        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            services.AddDbContext<SlaveDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection"),
                                                            b => b.MigrationsAssembly("ServerAdministration.Server.Slave"));
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ISiteInfoService, SiteInfoService>();
            services.AddScoped<IRepository<SiteIISLog>, RepositorySlave<SiteIISLog>>();
            services.AddSingleton<IConfiguration>(Configuration);

            // Add functionality to inject IOptions<T>
            services.AddOptions();

            var serverConfigurationSection = Configuration.GetSection("ServerConfiguration");
            
            services.ConfigureWritable<ServerConfiguration>(Configuration.GetSection("ServerConfiguration"), "appsettings.json");

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
