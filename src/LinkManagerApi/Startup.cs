using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using LinkManagerApi.Data;
using LinkManagerApi.SetupServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace LinkManagerApi;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;

    }
    public IConfiguration Configuration { get; }

    private readonly IWebHostEnvironment _env;

    public void ConfigureServices(IServiceCollection services)
    {
        services.InstallServicesFromAssembly(Configuration, _env);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkManagerApi"));
        // app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();

        PrepareDatabase.PrepareDB(app, env.IsDevelopment());
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains("ready"),
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonSerializer.Serialize(
                        new
                        {
                            status = report.Status.ToString(),
                            checks = report.Entries.Select(entry => new
                            {
                                name = entry.Key,
                                status = entry.Value.Status.ToString(),
                                exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                                duration = entry.Value.Duration.ToString()
                            })
                        }
                    );

                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            });

            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = (_) => false
            });

        });
    }
}
