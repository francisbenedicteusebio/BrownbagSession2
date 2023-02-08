using AutoMapper;
using BrownbagSession2.Configuration;
using Microsoft.Extensions.Options;

namespace BrownbagSession2.Services
{
    public class ServiceBase
    {
        protected readonly ConversionRateServiceSettings ConversionRateServiceSettings;
        protected readonly IMapper Mapper;

        public ServiceBase(IOptions<AppSettings> appSettings, IMapper mapper)
        {
            ConversionRateServiceSettings = appSettings?.Value != null
                ? appSettings.Value.ConversionRateServiceSettings
                : throw new ArgumentNullException(nameof(appSettings));

            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}
