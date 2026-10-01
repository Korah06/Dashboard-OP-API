using Dashboard_OP.src.api.DTOs;

namespace Dashboard_OP.src.api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequest request);
    }
}
