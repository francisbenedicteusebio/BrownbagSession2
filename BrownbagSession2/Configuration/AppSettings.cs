namespace BrownbagSession2.Configuration
{
    public class AppSettings
    {
        public ConversionRateServiceSettings ConversionRateServiceSettings { get; set; }
    }

    public class ConversionRateServiceSettings
    {
        public ServiceEndpoints ServiceEndpoints { get; set; }
    }

    public class ServiceEndpoints
    {
        public string ConversionRateServiceUrl { get; set; }
    }
}
