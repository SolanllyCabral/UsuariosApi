using Microsoft.EntityFrameworkCore;
using UsuariosApi.Context;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;
using UsuariosApi.Models;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductoResponse>> GetAllAsync()
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .ToListAsync();
        }

        public async Task<ProductoResponse?> GetByIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .Where(p => p.Id == id)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductoResponse> CreateAsync(ProductoRequest request)
        {
            var proveedorExiste = await _context.Proveedores
                .AnyAsync(p => p.Id == request.IdProveedor);

            if (!proveedorExiste)
            {
                throw new InvalidOperationException("El proveedor indicado no existe.");
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == request.IdCategoria);

            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría indicada no existe.");
            }

            var producto = new Producto
            {
                Nombre = request.Nombre,
                Precio = request.Precio,
                Stock = request.Stock,
                IdProveedor = request.IdProveedor,
                IdCategoria = request.IdCategoria
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(producto.Id)
                   ?? throw new Exception("Error al crear el producto.");
        }

        public async Task<ProductoResponse> UpdateAsync(int id, ProductoRequest request)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                throw new KeyNotFoundException("No se encontró un producto con ese ID.");
            }

            var proveedorExiste = await _context.Proveedores
                .AnyAsync(p => p.Id == request.IdProveedor);

            if (!proveedorExiste)
            {
                throw new InvalidOperationException("El proveedor indicado no existe.");
            }

            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == request.IdCategoria);

            if (!categoriaExiste)
            {
                throw new InvalidOperationException("La categoría indicada no existe.");
            }

            producto.Nombre = request.Nombre;
            producto.Precio = request.Precio;
            producto.Stock = request.Stock;
            producto.IdProveedor = request.IdProveedor;
            producto.IdCategoria = request.IdCategoria;

            await _context.SaveChangesAsync();

            return await GetByIdAsync(producto.Id)
                   ?? throw new Exception("Error al actualizar el producto.");
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                throw new KeyNotFoundException("No se encontró un producto con ese ID.");
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductoResumenResponse> GetResumenAsync()
        {
            if (!await _context.Productos.AnyAsync())
            {
                throw new InvalidOperationException("No hay productos registrados.");
            }

            var productoPrecioMasAlto = await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.Precio)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .FirstOrDefaultAsync();

            var productoPrecioMasBajo = await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .OrderBy(p => p.Precio)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .FirstOrDefaultAsync();

            var sumaTotalPrecios = await _context.Productos
                .SumAsync(p => p.Precio);

            var precioPromedio = await _context.Productos
                .AverageAsync(p => p.Precio);

            return new ProductoResumenResponse
            {
                ProductoPrecioMasAlto = productoPrecioMasAlto,
                ProductoPrecioMasBajo = productoPrecioMasBajo,
                SumaTotalPrecios = sumaTotalPrecios,
                PrecioPromedio = precioPromedio
            };
        }

        public async Task<IEnumerable<ProductoResponse>> GetByCategoriaAsync(int idCategoria)
        {
            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.Id == idCategoria);

            if (!categoriaExiste)
            {
                throw new KeyNotFoundException("No se encontró una categoría con ese ID.");
            }

            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .Where(p => p.IdCategoria == idCategoria)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductoResponse>> GetByProveedorAsync(int idProveedor)
        {
            var proveedorExiste = await _context.Proveedores
                .AnyAsync(p => p.Id == idProveedor);

            if (!proveedorExiste)
            {
                throw new KeyNotFoundException("No se encontró un proveedor con ese ID.");
            }

            return await _context.Productos
                .Include(p => p.Proveedor)
                .Include(p => p.Categoria)
                .Where(p => p.IdProveedor == idProveedor)
                .Select(p => new ProductoResponse
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Stock = p.Stock,
                    IdProveedor = p.IdProveedor,
                    Proveedor = p.Proveedor!.Nombre,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria!.Nombre
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalAsync()
        {
            return await _context.Productos.CountAsync();
        }
    }
}