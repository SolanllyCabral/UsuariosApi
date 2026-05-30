namespace UsuariosAPI.DTOs.Requests
{
    public class UsuarioRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public DateTime FechaDeNacimiento { get; set; }
    }
}
