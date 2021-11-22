using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using LinkManagerClientWPF.Api;
using LinkManagerClientWPF.Data;
using LinkManagerClientWPF.Services;
using LinkManagerClientWPF.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace LinkManagerClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider ServiceProvider;
        // [DllImport("user32", CharSet = CharSet.Unicode)]
        // static extern IntPtr FindWindow(string cls, string win);
        // [DllImport("user32")]
        // static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        // [DllImport("user32")]
        // static extern bool IsIconic(IntPtr hWnd);
        // [DllImport("user32")]
        // static extern bool OpenIcon(IntPtr hWnd);

        // private static Mutex _mutex = null;

        public App()
        {
            IConfigurationRoot configuration = BuildConfiguration();
           ServiceProvider= BuildServiceCollection(configuration);

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
            services.AddSingleton<ILinkServices, LinkServices>();
            services.AddScoped<ILinkRepository, LinkRepository>();

        }
        protected override void OnStartup(StartupEventArgs e)
        {

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            // const string appName = "MargiranLinkManager";
            // bool createdNew;

            // _mutex = new Mutex(true, appName, out createdNew);

            // if (!createdNew)
            // {
            //     ActivateOtherWindow();
            //     //app is already running! Exiting the application  
            //     Application.Current.Shutdown();
            // }


            // var client= serviceProvider.GetRequiredService<ILinksApi>();

            // var response =  client.SearchUpdateLinks(DateTimeOffset.Parse("19990101"));

            // var responseText= JsonSerializer.Serialize(response,new JsonSerializerOptions{
            //     WriteIndented = true
            // });



            base.OnStartup(e);
        }

        private static void ActivateOtherWindow()
        {
            // var other = FindWindow(null, "Link Manager Client");
            // if (other != IntPtr.Zero)
            // {
            //     SetForegroundWindow(other);
            //     if (IsIconic(other))
            //         OpenIcon(other);
            // }
        }

    }
}
