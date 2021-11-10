using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LinkManagerApi.SetupServices
{
    public class MvcInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services,IWebHostEnvironment env)
        {
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkManagerApi", Version = "v1" });
            });
            services.AddMediatR(typeof(Startup));
        }
    }
}