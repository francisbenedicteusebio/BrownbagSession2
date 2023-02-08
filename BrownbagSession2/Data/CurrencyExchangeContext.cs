using BrownbagSession2.DTO;
using Microsoft.EntityFrameworkCore;

namespace BrownbagSession2.Data
{
    public class CurrencyExchangeContext : DbContext
    {
        public CurrencyExchangeContext(DbContextOptions<CurrencyExchangeContext> options) : base(options)
        {
        }

        public DbSet<CurrencyCodesDto> CurrencyCodes { get; set; }
    }
}
