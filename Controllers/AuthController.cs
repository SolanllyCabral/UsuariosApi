using Microsoft.AspNetCore.Mvc;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.Exceptions;
using UsuariosApi.Services;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET /api/auth/generarContrasena?pass=miPassword
        [HttpGet("generarContrasena")]
        public ActionResult<string> GenerarContrasena(string pass)
        {
            var hash = AuthService.EncriptarSHA256(pass);
            return Ok(hash);
        }

        // POST /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }

        // POST /api/auth/refresh
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var response = await _authService.RefreshTokenAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }
    }
}