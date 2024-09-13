using BlazorProjectServer.Data;
using BlazorProjectSharedLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorProjectServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController(
        ILogger<WeatherForecastController> _logger,
        AppDbContext _dbcontext
    ) : ControllerBase
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

        [HttpGet("GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            try
            {
                bool canConnect = await _dbcontext.Database.CanConnectAsync();

                if (!canConnect)
                {
                    return StatusCode(500, "Unable to connect to the database.");
                }

                var forecast = Enumerable
                    .Range(1, 20)
                    .Select(index => new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        TemperatureC = Random.Shared.Next(-20, 55),
                        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                    })
                    .ToArray();

                var response = new
                {
                    Reason = "Database connected and data retrieved successfully.",
                    Data = forecast
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching weather data: {ex.Message}");

                return StatusCode(500, "An error occurred while fetching the weather data.");
            }
        }
    }
}
