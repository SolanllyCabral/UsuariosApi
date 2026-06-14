using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;

namespace UsuariosApi.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaResponse>> GetAllAsync();
        Task<CategoriaResponse?> GetByIdAsync(int id);
        Task<CategoriaResponse> CreateAsync(CategoriaRequest request);
        Task<CategoriaResponse> UpdateAsync(int id, CategoriaRequest request);
        Task DeleteAsync(int id);
    }
}