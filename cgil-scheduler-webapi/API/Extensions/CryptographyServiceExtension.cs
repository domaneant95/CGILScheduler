using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.Text;

namespace API.Extensions
{
    public static class CryptographyServiceExtension
    {
        public static IServiceCollection AddCryptographyServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<CryptographyService>();
            return services;
        }
    }
}
