using System.Text.Json.Serialization;
using Newtonsoft.Json;
using RestEase;

namespace EmployeeWebApp.Infrastructure;

public interface IOpenWeatherMapApi
{
    [Get("/geo/1.0/direct")]
    Task<List<LocationResponse>> GetLocationAsync([Query]string q, [Query]int limit, [Query]string appid);
    
    [Get("/data/2.5/weather")]
    Task<HttpResponseMessage> GetWeatherForecast([Query]string lat, [Query]string lon, [Query]string appid);
}

public class LocationResponse
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("local_names")] public LocalNames LocalNames { get; set; }
    [JsonProperty("lat")] public decimal Lat { get; set; }
    [JsonProperty("lon")] public decimal Lon { get; set; }
    [JsonProperty("country")] public string Country { get; set; }
}

public class LocalNames
{
    [JsonProperty("ka")] public string Ka { get; set; }
    [JsonProperty("en")] public string En { get; set; }
}