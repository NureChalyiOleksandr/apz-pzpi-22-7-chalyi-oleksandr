using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly DBContext _context;

    public AlertRepository(DBContext context)
    {
        _context = context;
    }

    // Create
    public async Task<Alert> CreateAsync(Alert alert)
    {
        _context.Alerts.Add(alert);
        await _context.SaveChangesAsync();
        return alert;
    }

    // Update
    public async Task<Alert> UpdateAsync(Alert alert)
    {
        _context.Alerts.Update(alert);
        await _context.SaveChangesAsync();
        return alert;
    }

    // Delete
    public async Task<bool> DeleteAsync(int alertId)
    {
        var alert = await _context.Alerts.FindAsync(alertId);
        if (alert == null) return false;

        _context.Alerts.Remove(alert);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get by Id
    public async Task<Alert> GetByIdAsync(int alertId)
    {
        return await _context.Alerts.FindAsync(alertId);
    }

    // Get all alerts
    public async Task<List<Alert>> GetAllAsync()
    {
        return await _context.Alerts.ToListAsync();
    }
}
