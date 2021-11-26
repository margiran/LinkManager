using System;
using System.Windows;
using LinkManagerClientWPF.Api;
using LinkManagerClientWPF.Data;
using LinkManagerClientWPF.Models;
using LinkManagerClientWPF.Services;
using LinkManagerClientWPF.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using LinkManagerClientWPF.ViewModels;

namespace LinkManagerClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider ServiceProvider;
        private readonly LinkManagerModel _linkManagerModel;

        public App()
        {
            IConfigurationRoot configuration = BuildConfiguration();
           ServiceProvider= BuildServiceCollection(configuration);
            _linkManagerModel = new LinkManagerModel(ServiceProvider.GetRequiredService<ILinkLocalDbService>(), ServiceProvider.GetRequiredService<ILinkManagerApiServices>());
        }

        private  ServiceProvider BuildServiceCollection(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
        }

        private void ConfigureServices(IServiceCollection services,IConfiguration configuration)
        {
            services.AddRefitClient<ILinksApi>()
                .ConfigureHttpClient(httpClient =>{
                    httpClient.BaseAddress=new Uri( configuration["LinkManagerApi:BaseAddress"]);
                });
            services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("default") ));
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ILinkManagerApiServices, LinkManagerApiServices>();
            services.AddTransient<ILinkLocalDbService, LinkLocalDbService>();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow MainWindow = new MainWindow()
            {
                DataContext =new MainWindowViewModel(_linkManagerModel)
            };
            MainWindow.Show();

            //var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            //mainWindow.DataContext = new MainWindowViewModel(_linkManagerModel);
          
            //mainWindow.Show();

            base.OnStartup(e);
        }

    }
}
