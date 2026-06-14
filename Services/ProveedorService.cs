using Microsoft.EntityFrameworkCore;
using UsuariosApi.Context;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;
using UsuariosApi.Models;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly AppDbContext _context;

        public ProveedorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProveedorResponse>> GetAllAsync()
        {
            return await _context.Proveedores
                .Select(p => new ProveedorResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Contacto = p.Contacto
                })
                .ToListAsync();
        }

        public async Task<ProveedorResponse?> GetByIdAsync(int id)
        {
            return await _context.Proveedores
                .Where(p => p.Id == id)
                .Select(p => new ProveedorResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Contacto = p.Contacto
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProveedorResponse> CreateAsync(ProveedorRequest request)
        {
            var existeProveedor = await _context.Proveedores
                .AnyAsync(p => p.Nombre.ToLower() == request.Nombre.ToLower());

            if (existeProveedor)
            {
                throw new InvalidOperationException("Ya existe un proveedor con ese nombre.");
            }

            var proveedor = new Proveedor
            {
                Nombre = request.Nombre,
                Contacto = request.Contacto
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return new ProveedorResponse
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Contacto = proveedor.Contacto
            };
        }

        public async Task<ProveedorResponse> UpdateAsync(int id, ProveedorRequest request)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                throw new KeyNotFoundException("No se encontró un proveedor con ese ID.");
            }

            var nombreExiste = await _context.Proveedores
                .AnyAsync(p => p.Nombre.ToLower() == request.Nombre.ToLower()
                            && p.Id != id);

            if (nombreExiste)
            {
                throw new InvalidOperationException("Ya existe otro proveedor con ese nombre.");
            }

            proveedor.Nombre = request.Nombre;
            proveedor.Contacto = request.Contacto;

            await _context.SaveChangesAsync();

            return new ProveedorResponse
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Contacto = proveedor.Contacto
            };
        }

        public async Task DeleteAsync(int id)
        {
            var proveedor = await _context.Proveedores
                .Include(p => p.Productos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (proveedor == null)
            {
                throw new KeyNotFoundException("No se encontró un proveedor con ese ID.");
            }

            if (proveedor.Productos.Any())
            {
                throw new InvalidOperationException("No se puede eliminar el proveedor porque tiene productos registrados.");
            }

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
        }
    }
}