using Microsoft.EntityFrameworkCore;
using UsuariosApi.Context;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;
using UsuariosApi.Models;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaResponse>> GetAllAsync()
        {
            return await _context.Categorias
                .Select(c => new CategoriaResponse
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .ToListAsync();
        }

        public async Task<CategoriaResponse?> GetByIdAsync(int id)
        {
            return await _context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaResponse
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CategoriaResponse> CreateAsync(CategoriaRequest request)
        {
            var existeCategoria = await _context.Categorias
                .AnyAsync(c => c.Nombre.ToLower() == request.Nombre.ToLower());

            if (existeCategoria)
            {
                throw new InvalidOperationException("Ya existe una categoría con ese nombre.");
            }

            var categoria = new Categoria
            {
                Nombre = request.Nombre
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return new CategoriaResponse
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }

        public async Task<CategoriaResponse> UpdateAsync(int id, CategoriaRequest request)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("No se encontró una categoría con ese ID.");
            }

            var nombreExiste = await _context.Categorias
                .AnyAsync(c => c.Nombre.ToLower() == request.Nombre.ToLower()
                            && c.Id != id);

            if (nombreExiste)
            {
                throw new InvalidOperationException("Ya existe otra categoría con ese nombre.");
            }

            categoria.Nombre = request.Nombre;

            await _context.SaveChangesAsync();

            return new CategoriaResponse
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }

        public async Task DeleteAsync(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("No se encontró una categoría con ese ID.");
            }

            if (categoria.Productos.Any())
            {
                throw new InvalidOperationException("No se puede eliminar la categoría porque tiene productos registrados.");
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}