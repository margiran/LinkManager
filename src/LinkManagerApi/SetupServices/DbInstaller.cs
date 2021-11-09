using LinkManagerApi.Data;
using LinkManagerApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.SetupServices
{
    public class DbInstaller : IServiceInstallers
    {
        public void InstallService(IConfiguration Configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")) );
            services.AddScoped<ILinkService,SqlServerLinkService>();
        }
    }
}