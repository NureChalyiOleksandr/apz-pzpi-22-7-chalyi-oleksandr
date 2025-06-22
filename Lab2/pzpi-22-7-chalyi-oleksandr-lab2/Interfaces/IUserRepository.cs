using SmartLightSense.Models;

namespace SmartLightSense.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int userId);
    Task<User> GetByIdAsync(int userId);
    Task<List<User>> GetAllAsync();
    Task<User> GetByUserNameAsync(string userName);
}
