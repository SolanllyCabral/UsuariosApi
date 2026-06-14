using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;

namespace UsuariosApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
        string GenerarToken(int id, string username);
    }
}