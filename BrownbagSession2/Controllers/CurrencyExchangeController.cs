using BrownbagSession2.DTO;
using BrownbagSession2.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrownbagSession2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<CurrencyExchangeController> _logger;

        public CurrencyExchangeController(ICurrencyService currencyService, ILogger<CurrencyExchangeController> logger)
        {
            _currencyService = currencyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<CurrencyCodesDto>> GetCurrencyCodes()
        {
            var result = await _currencyService.GetLatestCurrencyExchangeRates();
            return result;
        }

        [HttpPost]
        public async Task<ActionResult> PostCurrencyExchangeDailyUpdate()
        {
            try
            {
                _logger.LogInformation("Retrieving the Currency Exchange Rate as of {now}", DateTime.Now);
                var result = await _currencyService.UpdateCurrencyExchangeRates();
                return new OkObjectResult(result);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }
        }

        
    }
}
