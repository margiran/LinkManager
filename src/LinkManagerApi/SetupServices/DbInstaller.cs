using System;
using LinkManagerApi.Data;
using LinkManagerApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using HealthChecks.SqlServer;
using LinkManagerApi.Settings;

namespace LinkManagerApi.SetupServices;

public class DbInstaller : IServiceInstallers
{
    public void InstallService(IConfiguration Configuration, IServiceCollection services, IWebHostEnvironment env)
    {
        var dbSetting= Configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();

        if (env.IsDevelopment())
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("linkDB")
            );
        }
        else
        {
            Console.WriteLine($"using sql server DB ");

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(dbSetting.ConnectionString));

        }
        services.AddScoped<ILinkService, SqlServerLinkService>();
        // Registers required services for health checks
        services.AddHealthChecks()
            // Add a health check for a SQL Server database
            .AddSqlServer(
                connectionString: dbSetting.ConnectionString,
                name: "LinkManagerDB-check",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });
    }

}

