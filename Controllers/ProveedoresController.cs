using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.DTOs.Requests;
using UsuariosApi.DTOs.Responses;
using UsuariosApi.Services.Interfaces;

namespace UsuariosApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorResponse>>> GetProveedores()
        {
            var proveedores = await _proveedorService.GetAllAsync();

            return Ok(proveedores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorResponse>> GetProveedor(int id)
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);

            if (proveedor == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró un proveedor con ese ID."
                });
            }

            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorResponse>> CrearProveedor(ProveedorRequest request)
        {
            try
            {
                var proveedor = await _proveedorService.CreateAsync(request);

                return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, proveedor);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, ProveedorRequest request)
        {
            try
            {
                var proveedor = await _proveedorService.UpdateAsync(id, request);

                return Ok(new
                {
                    mensaje = "Proveedor actualizado correctamente.",
                    proveedor
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            try
            {
                await _proveedorService.DeleteAsync(id);

                return Ok(new
                {
                    mensaje = "Proveedor eliminado correctamente."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }
    }
}