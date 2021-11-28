
using System.Text.Json;
using LinkManagerClientCLI.Api;
using LinkManagerClientCLI.Services;
using LinkManagerClientCLI.TestableOutput;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
namespace LinkManagerClientCLI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = BuildConfiguration();
            var serviceProvider = BuildServiceCollection(configuration);



            // var client = serviceProvider.GetRequiredService<ILinksApi>();

            // var response=Task.FromResult( client.SearchUpdateLinks(DateTimeOffset.Parse("1900/01/01")));

            // var responseText= JsonSerializer.Serialize(response, new JsonSerializerOptions{
            //     WriteIndented=true
            // });

            // Console.Write(responseText);
            var app = serviceProvider.GetRequiredService<LinkManagerApiClientApplication>();
            await app.RunAsync(args);
        }

        private static ServiceProvider BuildServiceCollection(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureService(configuration, services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureService(IConfigurationRoot configuration, IServiceCollection services)
        {
            services.AddSingleton<LinkManagerApiClientApplication>();
            services.AddSingleton<IConsoleWriter,ConsoleWriter>();
            services.AddSingleton<ILinkServices,LinkServices>();
            services.AddRefitClient<ILinksApi>()
            .ConfigureHttpClient(httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["LinkManagerApi:BaseAddress"]);
            });
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
        }

    }
}
