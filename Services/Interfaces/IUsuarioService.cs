using UsuariosAPI.DTOs.Requests;
using UsuariosAPI.DTOs.Responses;

namespace UsuariosAPI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponse>> GetAllAsync();
        Task<UsuarioResponse?> GetByIdAsync(int id);
        Task<UsuarioResponse> CreateAsync(UsuarioRequest request);
        Task<UsuarioResponse> UpdateAsync(int id, UsuarioRequest request);
        Task DeleteAsync(int id);
    }
}
