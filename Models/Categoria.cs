using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre de la categoría debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre de la categoría no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}