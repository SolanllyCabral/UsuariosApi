namespace UsuariosApi.DTOs.Responses
{
    public class ProductoResumenResponse
    {
        public ProductoResponse? ProductoPrecioMasAlto { get; set; }
        public ProductoResponse? ProductoPrecioMasBajo { get; set; }
        public decimal SumaTotalPrecios { get; set; }
        public decimal PrecioPromedio { get; set; }
    }
}