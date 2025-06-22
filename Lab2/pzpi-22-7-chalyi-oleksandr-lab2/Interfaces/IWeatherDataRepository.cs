using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IWeatherDataRepository
{
    Task<WeatherData> CreateAsync(WeatherData weatherData);
    Task<WeatherData> UpdateAsync(WeatherData weatherData);
    Task<bool> DeleteAsync(int weatherDataId);
    Task<WeatherData> GetByIdAsync(int weatherDataId);
    Task<List<WeatherData>> GetAllAsync();
}
