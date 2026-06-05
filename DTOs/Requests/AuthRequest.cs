using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.DTOs.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }

    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "El refresh token es requerido.")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
