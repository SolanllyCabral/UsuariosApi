using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.DTOs.Requests
{
    public class ProductoRequest
    {
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre del producto debe tener al menos 2 caracteres.")]
        [MaxLength(100, ErrorMessage = "El nombre del producto no puede exceder 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio del producto es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock del producto es requerido.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "El proveedor es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor válido.")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "La categoría es requerida.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida.")]
        public int IdCategoria { get; set; }
    }
}