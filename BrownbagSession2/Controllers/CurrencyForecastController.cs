using BrownbagSession2.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BrownbagSession2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyForecastController : ControllerBase
    {
        private static readonly string[] Summaries = {
        "Trending", "Losing", "TopGainer-24hrs", "TopLoser24hrs", "Winner-Top5", "52-Week-Champ", "Top-Consisitent-52Weeks", "MostActive", "Winning", "Apocalypse"
    };

        private readonly ILogger<CurrencyForecastController> _logger;

        public CurrencyForecastController(ILogger<CurrencyForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<CurrencyForecast> Get()
        {
            _logger.LogInformation("GetWeatherForecast called");

            return Enumerable.Range(1, 5).Select(index => new CurrencyForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}