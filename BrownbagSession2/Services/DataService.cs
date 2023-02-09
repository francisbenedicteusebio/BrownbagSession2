using AutoMapper;
using BrownbagSession2.Contracts;
using BrownbagSession2.Data;
using BrownbagSession2.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;
using Serilog;
using ILogger = Serilog.ILogger;


namespace BrownbagSession2.Services
{
    public interface IDataService
    {
        Task<int> UpdateRatesAsync(List<CurrencyCodesDto> resultMapped);

        Task<List<CurrencyCodesDto>> GetRatesAsync();
    }
    public class DataService : IDataService
    {
        private readonly CurrencyExchangeContext _context;
        private static readonly ILogger Logger = Log.ForContext<DataService>();
        public DataService(CurrencyExchangeContext context)
        {
            _context = context;
        }

        public async Task<int> UpdateRatesAsync(List<CurrencyCodesDto> resultMapped)
        {
            await _context.CurrencyCodes.AddRangeAsync(resultMapped);
            await _context.SaveChangesAsync();

            Logger.Information("Currency Exchange Rate successfully added currency {rateCount} codes.", resultMapped.Count);
            return resultMapped.Count;
        }

        public async Task<List<CurrencyCodesDto>> GetRatesAsync()
        {
            Logger.Information("Retrieving Currency Exchange Rates from database.");
            return await _context.CurrencyCodes.ToListAsync();
        }
    }
}
