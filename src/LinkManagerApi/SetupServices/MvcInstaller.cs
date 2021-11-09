using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LinkManagerApi.SetupServices
{
    public class MvcInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinkManagerApi", Version = "v1" });
            });
        }
    }
}