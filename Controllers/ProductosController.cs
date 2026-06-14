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
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoResponse>>> GetProductos()
        {
            var productos = await _productoService.GetAllAsync();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoResponse>> GetProducto(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);

            if (producto == null)
            {
                return NotFound(new
                {
                    mensaje = "No se encontró un producto con ese ID."
                });
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoResponse>> CrearProducto(ProductoRequest request)
        {
            try
            {
                var producto = await _productoService.CreateAsync(request);

                return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
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
        public async Task<IActionResult> ActualizarProducto(int id, ProductoRequest request)
        {
            try
            {
                var producto = await _productoService.UpdateAsync(id, request);

                return Ok(new
                {
                    mensaje = "Producto actualizado correctamente.",
                    producto
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
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                await _productoService.DeleteAsync(id);

                return Ok(new
                {
                    mensaje = "Producto eliminado correctamente."
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("resumen")]
        public async Task<ActionResult<ProductoResumenResponse>> GetResumenProductos()
        {
            try
            {
                var resumen = await _productoService.GetResumenAsync();

                return Ok(resumen);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("categoria/{idCategoria}")]
        public async Task<IActionResult> GetProductosPorCategoria(int idCategoria)
        {
            try
            {
                var productos = await _productoService.GetByCategoriaAsync(idCategoria);

                return Ok(productos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("proveedor/{idProveedor}")]
        public async Task<IActionResult> GetProductosPorProveedor(int idProveedor)
        {
            try
            {
                var productos = await _productoService.GetByProveedorAsync(idProveedor);

                return Ok(productos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    mensaje = ex.Message
                });
            }
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetCantidadTotalProductos()
        {
            var total = await _productoService.GetTotalAsync();

            return Ok(new
            {
                cantidadTotalProductos = total
            });
        }
    }
}