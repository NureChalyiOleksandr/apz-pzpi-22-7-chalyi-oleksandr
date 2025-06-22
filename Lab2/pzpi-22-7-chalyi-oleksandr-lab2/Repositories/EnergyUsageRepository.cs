using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class EnergyUsageRepository : IEnergyUsageRepository
{
    private readonly DBContext _context;

    public EnergyUsageRepository(DBContext context)
    {
        _context = context;
    }

    // Create
    public async Task<EnergyUsage> CreateAsync(EnergyUsage energyUsage)
    {
        _context.EnergyUsages.Add(energyUsage);
        await _context.SaveChangesAsync();
        return energyUsage;
    }

    // Update
    public async Task<EnergyUsage> UpdateAsync(EnergyUsage energyUsage)
    {
        _context.EnergyUsages.Update(energyUsage);
        await _context.SaveChangesAsync();
        return energyUsage;
    }

    // Delete
    public async Task<bool> DeleteAsync(int energyUsageId)
    {
        var energyUsage = await _context.EnergyUsages.FindAsync(energyUsageId);
        if (energyUsage == null) return false;

        _context.EnergyUsages.Remove(energyUsage);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get by Id
    public async Task<EnergyUsage> GetByIdAsync(int energyUsageId)
    {
        return await _context.EnergyUsages.FindAsync(energyUsageId);
    }

    // Get all energy usages
    public async Task<List<EnergyUsage>> GetAllAsync()
    {
        return await _context.EnergyUsages.ToListAsync();
    }
}
