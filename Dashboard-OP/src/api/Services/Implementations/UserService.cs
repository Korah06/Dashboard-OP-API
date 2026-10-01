using Dashboard_OP.src.api.Models;
using Dashboard_OP.src.api.Services.Interfaces;
using Dashboard_OP.src.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_OP.src.api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(Guid id, User user)
        {
            User? existing = await _context.Users.FindAsync(id);
            if (existing == null) return false;

            // Copia aquí las propiedades de tu User
            existing.UserName = user.UserName;
            existing.Email = user.Email;
            existing.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
