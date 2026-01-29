using EmployeeWebApp.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private IHttpClientFactory _httpClientFactory;
    private IOpenWeatherMapApi _openWeatherMapApi;

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, IOpenWeatherMapApi openWeatherMapApi)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _openWeatherMapApi = openWeatherMapApi;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("real-weather")]
    public async Task<string> GetRealWeatherAsync()
    {
        try
        {
            var location = await _openWeatherMapApi.GetLocationAsync("Tbilisi,GE-TB,+995", 1, "7aaa81dbe48a19a79a1aaa68253217b7");

            var weatherResponse = await _openWeatherMapApi.GetWeatherForecast(location[0].Lat.ToString(), location[0].Lon.ToString(),
                "7aaa81dbe48a19a79a1aaa68253217b7");
            var weatherContent = await weatherResponse.Content.ReadAsStringAsync();
            Response.ContentType = "application/json";
            return weatherContent;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}

