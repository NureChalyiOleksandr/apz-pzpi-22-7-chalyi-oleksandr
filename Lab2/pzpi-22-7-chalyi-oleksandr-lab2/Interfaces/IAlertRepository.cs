using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IAlertRepository
{
    Task<Alert> CreateAsync(Alert alert);
    Task<Alert> UpdateAsync(Alert alert);
    Task<bool> DeleteAsync(int alertId);
    Task<Alert> GetByIdAsync(int alertId);
    Task<List<Alert>> GetAllAsync();
}
