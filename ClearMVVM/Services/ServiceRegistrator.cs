using ClearMVVM.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ClearMVVM.Services
{
    internal static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IDataService, DataService>()
            .AddTransient<IUserDialog, UserDialogService>();
    }
}
