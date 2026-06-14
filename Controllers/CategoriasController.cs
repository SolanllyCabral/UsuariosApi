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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaResponse>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetAllAsync();

            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaResponse>> GetCategoria(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró una categoría con ese ID."
                });
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaResponse>> CrearCategoria(CategoriaRequest request)
        {
            try
            {
                var categoria = await _categoriaService.CreateAsync(request);

                return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
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
        public async Task<IActionResult> ActualizarCategoria(int id, CategoriaRequest request)
        {
            try
            {
                var categoria = await _categoriaService.UpdateAsync(id, request);

                return Ok(new
                {
                    mensaje = "Categoría actualizada correctamente.",
                    categoria
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
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            try
            {
                await _categoriaService.DeleteAsync(id);

                return Ok(new
                {
                    mensaje = "Categoría eliminada correctamente."
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