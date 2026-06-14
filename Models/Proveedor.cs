using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre del proveedor debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre del proveedor no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contacto del proveedor es requerido.")]
        [MinLength(5, ErrorMessage = "El contacto del proveedor debe tener al menos 5 caracteres.")]
        [MaxLength(150, ErrorMessage = "El contacto del proveedor no puede exceder 150 caracteres.")]
        public string Contacto { get; set; } = string.Empty;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}