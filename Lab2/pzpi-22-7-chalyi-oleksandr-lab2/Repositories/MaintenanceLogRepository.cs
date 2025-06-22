using Microsoft.EntityFrameworkCore;
using SmartLightSense.Interfaces;
using SmartLightSense.Models;

namespace SmartLightSense.Repositories;

public class MaintenanceLogRepository : IMaintenanceLogRepository
{
    private readonly DBContext _context;

    public MaintenanceLogRepository(DBContext context)
    {
        _context = context;
    }

    // Create
    public async Task<MaintenanceLog> CreateAsync(MaintenanceLog maintenanceLog)
    {
        _context.MaintenanceLogs.Add(maintenanceLog);
        await _context.SaveChangesAsync();
        return maintenanceLog;
    }

    // Update
    public async Task<MaintenanceLog> UpdateAsync(MaintenanceLog maintenanceLog)
    {
        _context.MaintenanceLogs.Update(maintenanceLog);
        await _context.SaveChangesAsync();
        return maintenanceLog;
    }

    // Delete
    public async Task<bool> DeleteAsync(int maintenanceLogId)
    {
        var maintenanceLog = await _context.MaintenanceLogs.FindAsync(maintenanceLogId);
        if (maintenanceLog == null) return false;

        _context.MaintenanceLogs.Remove(maintenanceLog);
        await _context.SaveChangesAsync();
        return true;
    }

    // Get by Id
    public async Task<MaintenanceLog> GetByIdAsync(int maintenanceLogId)
    {
        return await _context.MaintenanceLogs.FindAsync(maintenanceLogId);
    }

    // Get all maintenance logs
    public async Task<List<MaintenanceLog>> GetAllAsync()
    {
        return await _context.MaintenanceLogs.ToListAsync();
    }
}
