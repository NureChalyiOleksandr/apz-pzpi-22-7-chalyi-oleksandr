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

    public async Task<WeatherData> CreateAsync(WeatherData weatherData)
    {
        _context.WeatherData.Add(weatherData);
        await _context.SaveChangesAsync();

        return weatherData;
    }

    public async Task<WeatherData> UpdateAsync(WeatherData weatherData)
    {
        _context.WeatherData.Update(weatherData);
        await _context.SaveChangesAsync();
        return weatherData;
    }

    public async Task<bool> DeleteAsync(int weatherDataId)
    {
        var weatherData = await _context.WeatherData.FindAsync(weatherDataId);
        if (weatherData == null) return false;

        _context.WeatherData.Remove(weatherData);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<WeatherData> GetByIdAsync(int weatherDataId)
    {
        return await _context.WeatherData.FindAsync(weatherDataId);
    }

    public async Task<List<WeatherData>> GetAllAsync()
    {
        return await _context.WeatherData.ToListAsync();
    }

    public async Task<WeatherData> GetLatestWeatherAsync()
    {
        return await _context.WeatherData
                             .OrderByDescending(w => w.Date)
                             .FirstOrDefaultAsync();
    }

}
