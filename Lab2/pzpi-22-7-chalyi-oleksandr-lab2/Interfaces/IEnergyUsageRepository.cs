using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IEnergyUsageRepository
{
    Task<EnergyUsage> CreateAsync(EnergyUsage energyUsage);
    Task<EnergyUsage> UpdateAsync(EnergyUsage energyUsage);
    Task<bool> DeleteAsync(int energyUsageId);
    Task<EnergyUsage> GetByIdAsync(int energyUsageId);
    Task<List<EnergyUsage>> GetAllAsync();
}
