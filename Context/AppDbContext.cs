using Microsoft.EntityFrameworkCore;
using UsuariosApi.Models;

namespace UsuariosApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioAuth> UsuariosAuth { get; set; }
    }
}
