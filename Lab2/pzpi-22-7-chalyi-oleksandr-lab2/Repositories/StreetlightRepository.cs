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

    // Create
    public async Task<Streetlight> CreateAsync(Streetlight streetlight)
    {
        _context.Streetlights.Add(streetlight);
        await _context.SaveChangesAsync();
        return streetlight;
    }

    // Update
    public async Task<Streetlight> UpdateAsync(Streetlight streetlight)
    {
        _context.Streetlights.Update(streetlight);
        await _context.SaveChangesAsync();
        return streetlight;
    }

    // Delete
    public async Task<bool> DeleteAsync(int streetlightId)
    {
        var streetlight = await _context.Streetlights.FindAsync(streetlightId);
        if (streetlight == null) return false;

        _context.Streetlights.Remove(streetlight);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get by Id
    public async Task<Streetlight> GetByIdAsync(int streetlightId)
    {
        return await _context.Streetlights
                         .Include(s => s.Sensors)
                         .FirstOrDefaultAsync(sl => sl.Id == streetlightId);
    }

    // Get all streetlights
    public async Task<List<Streetlight>> GetAllAsync()
    {
        return await _context.Streetlights
                        .Include(s => s.Sensors)
                        .ToListAsync();
    }
}
