using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IMaintenanceLogRepository
{
    Task<MaintenanceLog> CreateAsync(MaintenanceLog maintenanceLog);
    Task<MaintenanceLog> UpdateAsync(MaintenanceLog maintenanceLog);
    Task<bool> DeleteAsync(int maintenanceLogId);
    Task<MaintenanceLog> GetByIdAsync(int maintenanceLogId);
    Task<List<MaintenanceLog>> GetAllAsync();
}
