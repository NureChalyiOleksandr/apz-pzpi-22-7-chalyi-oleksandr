using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class WeatherDataRepository : IWeatherDataRepository
{
    private readonly DBContext _context;

    public WeatherDataRepository(DBContext context)
    {
        _context = context;
    }

    // Create
    public async Task<WeatherData> CreateAsync(WeatherData weatherData)
    {
        _context.WeatherData.Add(weatherData);
        await _context.SaveChangesAsync();
        return weatherData;
    }

    // Update
    public async Task<WeatherData> UpdateAsync(WeatherData weatherData)
    {
        _context.WeatherData.Update(weatherData);
        await _context.SaveChangesAsync();
        return weatherData;
    }

    // Delete
    public async Task<bool> DeleteAsync(int weatherDataId)
    {
        var weatherData = await _context.WeatherData.FindAsync(weatherDataId);
        if (weatherData == null) return false;

        _context.WeatherData.Remove(weatherData);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get by Id
    public async Task<WeatherData> GetByIdAsync(int weatherDataId)
    {
        return await _context.WeatherData.FindAsync(weatherDataId);
    }

    // Get all weather data
    public async Task<List<WeatherData>> GetAllAsync()
    {
        return await _context.WeatherData.ToListAsync();
    }
}
