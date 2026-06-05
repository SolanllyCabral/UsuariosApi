using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class UsuarioAuth
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe tener al menos 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario no puede exceder 50 caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        public string PasswordHash { get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
