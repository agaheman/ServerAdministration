using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ServerAdministration.Server.Slave.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureWritable<T>(this IServiceCollection services,
            IConfigurationSection section,string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            var serviceProvider = services.BuildServiceProvider();
            var env = serviceProvider.GetService<IHostingEnvironment>();
            var option = serviceProvider.GetService<IOptionsMonitor<T>>();
            var writableOption = new WritableOptions<T>(env, option, section.Key, file);

            services.AddSingleton<IWritableOptions<T>>(writableOption);
            
        }
    }
}
