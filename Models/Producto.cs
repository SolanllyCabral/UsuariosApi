using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosApi.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre del producto debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio del producto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock del producto es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido.")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "La categoría es requerida.")]
        public int IdCategoria { get; set; }

        public Proveedor? Proveedor { get; set; }

        public Categoria? Categoria { get; set; }
    }
}