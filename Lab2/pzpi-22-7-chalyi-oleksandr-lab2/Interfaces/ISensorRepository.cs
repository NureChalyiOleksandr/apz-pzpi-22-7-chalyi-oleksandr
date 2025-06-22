using Microsoft.EntityFrameworkCore;
using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface ISensorRepository
{
    Task<Sensor> CreateAsync(Sensor sensor);
    Task<Sensor> UpdateAsync(Sensor sensor);
    Task<bool> DeleteAsync(int sensorId);
    Task<Sensor> GetByIdAsync(int sensorId);
    Task<List<Sensor>> GetAllAsync();
    Task<List<Sensor>> GetByStreetLightIdAsync(int streetlightId);
}
