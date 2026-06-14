namespace UsuariosApi.DTOs.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiracion { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
