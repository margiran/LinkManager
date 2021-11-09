using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkManagerApi.SetupServices
{
    public interface IServiceInstallers
    {
         void InstallService(IConfiguration Configuration,IServiceCollection services);
    }
}