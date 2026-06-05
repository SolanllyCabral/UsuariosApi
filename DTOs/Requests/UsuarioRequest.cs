using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.DTOs.Requests
{
    public class UsuarioRequest
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es requerido.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [MaxLength(200, ErrorMessage = "El correo no puede exceder 200 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida.")]
        public DateTime FechaDeNacimiento { get; set; }
    }
}