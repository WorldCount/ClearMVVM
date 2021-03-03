using System;
using System.Linq;
using System.Windows;
using ClearMVVM.Services;
using ClearMVVM.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClearMVVM
{
    public partial class App
    {
        private static IHost _host;

        #region Свойства
        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        public static IHost Host => _host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("app_settings.json", true, true))
            .Build();

        public static IServiceProvider Services => Host.Services;

        #endregion

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services) => services
            .AddServices()
            .AddViewModels();

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using (Host) await Host.StopAsync().ConfigureAwait(false);
        }
    }
}
