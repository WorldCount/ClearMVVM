﻿using System;
using System.Linq;
using System.Windows;
using ClearMVVM.Services;
using ClearMVVM.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ClearMVVM
{
    public partial class App
    {
        public static Window ActivedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsActive);

        public static Window FocusedWindow => Current.Windows.Cast<Window>().FirstOrDefault(w => w.IsFocused);

        private static IHost _host;

        public static IHost Host => _host ??= Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder(Environment.GetCommandLineArgs())
            .ConfigureAppConfiguration(cfg => cfg.AddJsonFile("app_settings.json", true, true))
            .ConfigureServices((host, services) => services
                .AddViews()
                .AddServices()
            )
            .Build();

        public static IServiceProvider Services => Host.Services;

        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;
            base.OnStartup(e);
            await host.StartAsync();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            using var host = Host;
            await host.StopAsync();
        }
    }
}
