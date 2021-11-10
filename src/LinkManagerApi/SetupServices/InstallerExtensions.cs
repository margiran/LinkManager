using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.SetupServices
{
    public static class InstallerExtensions
    {
        public static void InstallServicesFromAssembly(this IServiceCollection services, IConfiguration configuration,IWebHostEnvironment env)
        {
            var Installers = typeof(IServiceInstallers).Assembly.ExportedTypes.Where(s =>
                typeof(IServiceInstallers).IsAssignableFrom(s) && !s.IsAbstract && !s.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstallers>().ToList();

            Installers.ForEach(s => s.InstallService(configuration, services,env));

        }
    }
}