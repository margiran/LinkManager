using System;
using LinkManagerApi.Data;
using LinkManagerApi.HealthCheck;
using LinkManagerApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace LinkManagerApi.SetupServices;

public class DbInstaller : IServiceInstallers
{
    public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
    {
        var connectionString = Configuration.GetConnectionString("default");
        if (env.IsDevelopment())
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("linkDB")
            );
        }
        else
        {
            Console.WriteLine($"useing sql server DB {connectionString} , {env.IsProduction()}`");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("default")));

        }
        services.AddScoped<ILinkService, SqlServerLinkService>();
        // Registers required services for health checks
        connectionString += Configuration.GetValue<string>("SA_PASSWORD");
        Console.WriteLine($"useing sql server DB {connectionString} , {env.IsProduction()}`");
        services.AddHealthChecks()
            // Add a health check for a SQL Server database
            .AddCheck(
                "LinkManagerDB-check",
                new SqlConnectionHealthCheck(connectionString),
                HealthStatus.Unhealthy,
                new string[] { "LinkManagerDB" });

    }

}

