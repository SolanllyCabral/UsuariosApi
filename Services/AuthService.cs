using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UsuariosApi.Context;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;
using UsuariosApi.Exceptions;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            string passwordHash = EncriptarSHA256(request.Password);

            var usuario = await _context.UsuariosAuth
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.PasswordHash == passwordHash);

            if (usuario == null)
                throw new UnauthorizedException("Credenciales incorrectas.");

            var token = GenerarToken(usuario.Id, usuario.Username);
            var refreshToken = GenerarRefreshToken();
            var expiracion = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:ExpirationMinutes"));

            usuario.RefreshToken = refreshToken;
            usuario.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:RefreshExpirationMinutes"));

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken,
                Expiracion = expiracion,
                Username = usuario.Username
            };
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var usuario = await _context.UsuariosAuth
                .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken);

            if (usuario == null)
                throw new UnauthorizedException("Refresh token inválido.");

            if (usuario.RefreshTokenExpiration < DateTime.UtcNow)
                throw new UnauthorizedException("El refresh token ha expirado. Inicie sesión nuevamente.");

            var token = GenerarToken(usuario.Id, usuario.Username);
            var nuevoRefreshToken = GenerarRefreshToken();
            var expiracion = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:ExpirationMinutes"));

            usuario.RefreshToken = nuevoRefreshToken;
            usuario.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:RefreshExpirationMinutes"));

            await _context.SaveChangesAsync();

            return new AuthResponse
            {
                Token = token,
                RefreshToken = nuevoRefreshToken,
                Expiracion = expiracion,
                Username = usuario.Username
            };
        }

        public string GenerarToken(int id, string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
            var keySecurity = new SymmetricSecurityKey(key);

            var credentials = new SigningCredentials(
                keySecurity,
                SecurityAlgorithms.HmacSha256
            );

            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claim,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerarRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        public static string EncriptarSHA256(string texto)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(texto));
            return Convert.ToHexString(bytes).ToLower();
        }
    }
}