using System.Text.Json;
using AutoMapper;
using BrownbagSession2.Configuration;
using BrownbagSession2.Contracts;
using BrownbagSession2.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RestSharp;
using Serilog;
using ILogger = Serilog.ILogger;


namespace BrownbagSession2.Services
{
    public interface ICurrencyService
    {
        Task<bool> UpdateCurrencyExchangeRates();
        Task<List<CurrencyCodesDto>> GetLatestCurrencyExchangeRates();
    }
    
    public class CurrencyService : ServiceBase, ICurrencyService
    {
        private readonly RestClient _client;
        private readonly IDataService _dataService;
        private static readonly ILogger Logger = Log.ForContext<CurrencyService>();

        public CurrencyService(IOptions<AppSettings> appSettings, IMapper mapper, IDataService dataService) 
            : base(appSettings, mapper)
        {
            _dataService = dataService;
            _client = new RestClient(ConversionRateServiceSettings.ServiceEndpoints.ConversionRateServiceUrl);
        }


        public async Task<bool> UpdateCurrencyExchangeRates()
        {
            var request = new RestRequest();
            var response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception("Error Retrieving Rates :" + response.Content);
            }
            var deserializedResponse = JsonSerializer.Deserialize<ConversionRateApiObject>(response.Content);
            var currencyCodesList = new List<CurrencyCodes>();
            var properties = typeof(ConversionRate).GetProperties();

            foreach (var property in properties)
            {
                var value = (double)property.GetValue(deserializedResponse.conversion_rates);
                currencyCodesList.Add(new CurrencyCodes { Code = property.Name, Rate = value });
            }

            var resultMapped = Mapper.Map<List<CurrencyCodesDto>>(currencyCodesList);
            var updateRates = await _dataService.UpdateRatesAsync(resultMapped);

            if (updateRates > 0)
            {
                Logger.Information("Currency Exchange Rate successfully added currency {rateCount} codes.", updateRates);
                return true;
            }

            return false;
        }

        public async Task<List<CurrencyCodesDto>> GetLatestCurrencyExchangeRates()
        {
            var result = await _dataService.GetRatesAsync();
            return result;
        }
    }
}
