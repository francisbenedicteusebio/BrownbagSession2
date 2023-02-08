using AutoMapper;
using BrownbagSession2.Contracts;
using BrownbagSession2.Data;
using BrownbagSession2.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestSharp;

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
        public DataService(CurrencyExchangeContext context)
        {
            _context = context;
        }

        public async Task<int> UpdateRatesAsync(List<CurrencyCodesDto> resultMapped)
        { 
            await _context.CurrencyCodes.AddRangeAsync(resultMapped);
            await _context.SaveChangesAsync();
            return resultMapped.Count;
        }

        public async Task<List<CurrencyCodesDto>> GetRatesAsync()
        {
            return await _context.CurrencyCodes.ToListAsync();
        }
    }
}
