using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly DBContext _context;

    public SensorRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<Sensor> CreateAsync(Sensor sensor)
    {
        _context.Sensors.Add(sensor);
        await _context.SaveChangesAsync();
        return sensor;
    }

    public async Task<Sensor> UpdateAsync(Sensor sensor)
    {
        _context.Sensors.Update(sensor);
        await _context.SaveChangesAsync();

        return sensor;
    }

    public async Task<bool> DeleteAsync(int sensorId)
    {
        var sensor = await _context.Sensors.FindAsync(sensorId);
        if (sensor == null) return false;

        _context.Sensors.Remove(sensor);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Sensor> GetByIdAsync(int sensorId)
    {
        return await _context.Sensors.FindAsync(sensorId);
    }

    public async Task<List<Sensor>> GetAllAsync()
    {
        return await _context.Sensors.ToListAsync();
    }

    public async Task<List<Sensor>> GetByStreetLightIdAsync(int streetlightId)
    {
        return await _context.Sensors
            .Where(sensor => sensor.StreetlightId == streetlightId)
            .ToListAsync();
    }
    public async Task<List<Sensor>> GetByStreetlightIdsAsync(List<int> streetlightIds)
    {
        return await _context.Sensors
                             .Where(sensor => streetlightIds.Contains(sensor.StreetlightId))
                             .ToListAsync();
    }

}
