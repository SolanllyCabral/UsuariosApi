using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Context;
using UsuariosAPI.DTOs.Requests;
using UsuariosAPI.DTOs.Responses;
using UsuariosAPI.Exceptions;
using UsuariosAPI.Models;
using UsuariosAPI.Services.Interfaces;

namespace UsuariosAPI.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioResponse>> GetAllAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios.Select(u => MapToResponse(u));
        }

        public async Task<UsuarioResponse?> GetByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return null;

            return MapToResponse(usuario);
        }

        public async Task<UsuarioResponse> CreateAsync(UsuarioRequest request)
        {
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == request.Correo);

            if (correoExiste)
                throw new CorreoDuplicadoException(request.Correo);

            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Correo = request.Correo,
                FechaDeNacimiento = request.FechaDeNacimiento
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return MapToResponse(usuario);
        }

        public async Task<UsuarioResponse> UpdateAsync(int id, UsuarioRequest request)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new NotFoundException($"No se encontró un usuario con el ID {id}.");

            bool correoEnUso = await _context.Usuarios
                .AnyAsync(u => u.Correo == request.Correo && u.Id != id);

            if (correoEnUso)
                throw new CorreoDuplicadoException(request.Correo);

            usuario.Nombre = request.Nombre;
            usuario.Correo = request.Correo;
            usuario.FechaDeNacimiento = request.FechaDeNacimiento;

            await _context.SaveChangesAsync();

            return MapToResponse(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id)
                ?? throw new NotFoundException($"No se encontró un usuario con el ID {id}.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        private static UsuarioResponse MapToResponse(Usuario u) => new()
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo,
            FechaDeNacimiento = u.FechaDeNacimiento
        };
    }
}
