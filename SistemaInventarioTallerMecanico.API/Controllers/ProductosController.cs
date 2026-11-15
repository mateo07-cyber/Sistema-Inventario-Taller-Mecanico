using Microsoft.AspNetCore.Mvc;
using SistemaInventarioTallerMecanico.Application.Contract;
using SistemaInventarioTallerMecanico.Application.Dtos.Producto;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            return Ok(await _productoService.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _productoService.ObtenerPorIdAsync(id);
            return producto is null ? NotFound() : Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto(ProductoDto dto)
        {
            var creado = await _productoService.CrearAsync(dto);
            if (creado is null)
                return BadRequest("No se pudo crear el producto. Verifique que el proveedor exista y los datos sean válidos.");

            return CreatedAtAction(nameof(GetProducto), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ProductoDto dto)
        {
            var actualizado = await _productoService.ActualizarAsync(id, dto);
            if (!actualizado)
                return BadRequest("No se pudo actualizar el producto. Verifique el ID, el proveedor y los datos enviados.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoService.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
