using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.DTOs.Requests;
using UsuariosAPI.Exceptions;
using UsuariosAPI.Services.Interfaces;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET /api/usuarios
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        // GET /api/usuarios/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);

            if (usuario == null)
                return NotFound(new { mensaje = $"No se encontró un usuario con el ID {id}." });

            return Ok(usuario);
        }

        // POST /api/usuarios
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioRequest request)
        {
            try
            {
                var creado = await _usuarioService.CreateAsync(request);
                return CreatedAtAction(nameof(GetUsuario), new { id = creado.Id }, creado);
            }
            catch (CorreoDuplicadoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // PUT /api/usuarios/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioRequest request)
        {
            try
            {
                var actualizado = await _usuarioService.UpdateAsync(id, request);
                return Ok(actualizado);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
            catch (CorreoDuplicadoException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // DELETE /api/usuarios/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                await _usuarioService.DeleteAsync(id);
                return Ok(new { mensaje = "Usuario eliminado correctamente." });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }
    }
}
