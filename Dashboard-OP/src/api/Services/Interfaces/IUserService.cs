using Dashboard_OP.src.api.Models;

namespace Dashboard_OP.src.api.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> CreateAsync(User user);
        Task<bool> UpdateAsync(Guid id, User user);
        Task<bool> DeleteAsync(Guid id);
    }
}
