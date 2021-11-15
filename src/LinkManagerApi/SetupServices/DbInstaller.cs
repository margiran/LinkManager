using System;
using LinkManagerApi.Data;
// using LinkManagerApi.HealthCheck;
using LinkManagerApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using HealthChecks.SqlServer;

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
            // Read sql passord from user-secrets in development environment
            connectionString += Configuration.GetValue<string>("SA_PASSWORD");

        }
        else
        {
            Console.WriteLine($"useing sql server DB ");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("default")));

        }
        services.AddScoped<ILinkService, SqlServerLinkService>();
        // Registers required services for health checks
        services.AddHealthChecks()
            // Add a health check for a SQL Server database
            .AddSqlServer(
                connectionString,
                name: "LinkManagerDB-check",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });
    }

}

