using BlazorProjectSharedLibrary;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProjectServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetWeatherForecast")]
        public IActionResult Get()
        {
            try
            {
                var forecast = Enumerable
                    .Range(1, 20)
                    .Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();

                return Ok(forecast);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching weather data: {ex.Message}");

                return StatusCode(500, "An error occurred while fetching the weather data.");
            }
        }
    }
}
