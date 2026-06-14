using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;

namespace UsuariosApi.Services.Interfaces
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponse>> GetAllAsync();
        Task<ProductoResponse?> GetByIdAsync(int id);
        Task<ProductoResponse> CreateAsync(ProductoRequest request);
        Task<ProductoResponse> UpdateAsync(int id, ProductoRequest request);
        Task DeleteAsync(int id);

        Task<ProductoResumenResponse> GetResumenAsync();
        Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int idCategoria);
        Task<IEnumerable<ProductoResponse>> GetByProveedorAsync(int idProveedor);
        Task<int> GetTotalAsync();
    }
}