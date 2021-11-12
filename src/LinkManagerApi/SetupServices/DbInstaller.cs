using System;
using LinkManagerApi.Data;
using LinkManagerApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LinkManagerApi.SetupServices
{
    public class DbInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("linkDB")
                );
            }
            else
            {
                var con=Configuration.GetConnectionString("default");
                Console.WriteLine($"useing sql server DB {con} , {env.IsProduction()}`");

                services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("default")));
            }
            services.AddScoped<ILinkService, SqlServerLinkService>();
        }
    }
}