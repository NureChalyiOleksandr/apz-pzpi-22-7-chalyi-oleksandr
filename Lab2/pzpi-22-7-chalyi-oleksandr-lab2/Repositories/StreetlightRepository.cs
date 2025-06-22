using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class StreetlightRepository : IStreetlightRepository
{
    private readonly DBContext _context;

    public StreetlightRepository(DBContext context)
    {
        _context = context;
    }

    public async Task<Streetlight> CreateAsync(Streetlight streetlight)
    {
        _context.Streetlights.Add(streetlight);
        await _context.SaveChangesAsync();
        return streetlight;
    }

    public async Task<Streetlight> UpdateAsync(Streetlight streetlight)
    {
        _context.Streetlights.Update(streetlight);
        await _context.SaveChangesAsync();
        return streetlight;
    }

    public async Task<bool> DeleteAsync(int streetlightId)
    {
        var streetlight = await _context.Streetlights.FindAsync(streetlightId);
        if (streetlight == null) return false;

        _context.Streetlights.Remove(streetlight);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Streetlight> GetByIdAsync(int streetlightId)
    {
        return await _context.Streetlights
                         .Include(s => s.Sensors)
                         .FirstOrDefaultAsync(sl => sl.Id == streetlightId);
    }

    public async Task<List<Streetlight>> GetAllAsync()
    {
        return await _context.Streetlights
                        .Include(s => s.Sensors)
                        .ToListAsync();
    }

    public async Task UpdateBrightnessAsync(int streetlightId, int brightnessLevel)
    {
        var streetlight = await _context.Streetlights.FindAsync(streetlightId);
        if (streetlight == null) throw new ArgumentException("Streetlight not found");

        streetlight.BrightnessLevel = brightnessLevel;
        await _context.SaveChangesAsync();
    }

    public async Task<List<Streetlight>> GetBySectorIdAsync(int sectorId)
    {
        return await _context.Streetlights
                             .Where(s => s.SectorId == sectorId)
                             .Include(s => s.Sensors)
                             .ToListAsync();
    }

}
