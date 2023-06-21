using API.Services;
using Application.Filemanager;
using System.Runtime.CompilerServices;

namespace API.Extensions
{
    public static class DbFileProviderExtension
    {
        public static IServiceCollection AddDbFileProviderService(this IServiceCollection services)
        {
            services.AddSingleton<DbFileProviderService>();
            return services;
        }
    }
}
