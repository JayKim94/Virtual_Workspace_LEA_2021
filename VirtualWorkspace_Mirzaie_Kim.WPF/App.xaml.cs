using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VirtualWorkspace_Mirzaie_Kim.Domain.Models;
using VirtualWorkspace_Mirzaie_Kim.Domain.Services;
using VirtualWorkspace_Mirzaie_Kim.EntityFramework.Services;
using VirtualWorkspace_Mirzaie_Kim.SpotifyAPI;
using VirtualWorkspace_Mirzaie_Kim.WPF.State;
using VirtualWorkspace_Mirzaie_Kim.WPF.ViewModels;

namespace VirtualWorkspace_Mirzaie_Kim.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public static IServiceProvider ServiceProvider { get; private set; }

        public static Workspace CurrentWorkspace { get; set; }

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    RegisterServices(services);
                })
                .Build();

            ServiceProvider = _host.Services;

            string clientId = ConfigurationManager.AppSettings.Get("spotifyClientId");
            string clientSecret = ConfigurationManager.AppSettings.Get("spotifyClientSecret");
            new SpotifyHttpClient(clientId, clientSecret);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<WorkspaceViewModel>();

            string clientId = ConfigurationManager.AppSettings.Get("spotifyClientId");
            string clientSecret = ConfigurationManager.AppSettings.Get("spotifyClientSecret");
            services.AddSingleton<SpotifyHttpClientFactory>(new SpotifyHttpClientFactory(clientId, clientSecret));

            

            services.AddSingleton<INavigator, Navigator>();
            services.AddSingleton<IWorkspaceService, WorkspaceService>();
            services.AddSingleton<IResourceDirectoryService, ResourceDirectoryService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Window window = new MainWindow();
            window.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
