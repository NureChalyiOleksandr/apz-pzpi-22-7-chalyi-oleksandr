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
    public async Task<EnergyUsage?> GetByDateAndStreetlightIdAsync(int streetlightId, DateTime date)
    {
        return await _context.EnergyUsages
            .Where(eu => eu.StreetlightId == streetlightId && eu.Date <= date)
            .OrderByDescending(eu => eu.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<EnergyUsage?> GetLatestByStreetlightIdAsync(int streetlightId)
    {
        return await _context.EnergyUsages
            .Where(eu => eu.StreetlightId == streetlightId)
            .OrderByDescending(eu => eu.Date)
            .FirstOrDefaultAsync();
    }

    public async Task<List<EnergyUsage>> GetLatestForAllStreetlightsAsync()
    {
        return await _context.EnergyUsages
            .GroupBy(eu => eu.StreetlightId)
            .Select(group => group.OrderByDescending(eu => eu.Date).FirstOrDefault())
            .ToListAsync();
    }

    public async Task<List<EnergyUsage>> GetByDateForAllStreetlightsAsync(DateTime date)
    {
        return await _context.EnergyUsages
            .Where(eu => eu.Date <= date)
            .GroupBy(eu => eu.StreetlightId)
            .Select(group => group.OrderByDescending(eu => eu.Date).FirstOrDefault())
            .ToListAsync();
    }
}
