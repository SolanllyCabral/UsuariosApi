namespace UsuariosApi.DTOs.Responses
{
    public class ProductoResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdProveedor { get; set; }
        public string Proveedor { get; set; } = string.Empty;
        public int IdCategoria { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}