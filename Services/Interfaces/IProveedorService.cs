using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;

namespace UsuariosApi.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorResponse>> GetAllAsync();
        Task<ProveedorResponse?> GetByIdAsync(int id);
        Task<ProveedorResponse> CreateAsync(ProveedorRequest request);
        Task<ProveedorResponse> UpdateAsync(int id, ProveedorRequest request);
        Task DeleteAsync(int id);
    }
}