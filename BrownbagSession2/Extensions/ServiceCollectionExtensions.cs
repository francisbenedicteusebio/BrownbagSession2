using BrownbagSession2.Configuration;
using BrownbagSession2.Mapping;
using BrownbagSession2.Services;

namespace BrownbagSession2.Extensions
{
    internal static class ServiceCollectionExtensions
    {

        internal static void AddBrownbagServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDataService, DataService>();
            services.AddAutoMapper(typeof(CurrencyMappingProfile));
        }

        internal static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }
    }
}
