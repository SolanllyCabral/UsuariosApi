using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Response;

namespace UsuariosApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
        string GenerarToken(int id, string username);
    }
}