using SmartLightSense.Dtos;
using System.Text.Json.Serialization;

namespace SmartLightSense.Services.WeatherService;
public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public WeatherService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<WeatherDataCreateDto> GetWeatherForecastAsync(string city)
    {
        var apiKey = _configuration["WeatherApi:Key"];
        var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to retrieve weather data");
        }

        var result = await response.Content.ReadFromJsonAsync<WeatherApiResponse>();

        return new WeatherDataCreateDto(
            result.Visibility / 1000.0
        );
    }

    private class WeatherApiResponse
    {
        public int Visibility { get; set; }
    }
}
