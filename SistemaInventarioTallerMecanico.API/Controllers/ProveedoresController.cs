using Microsoft.AspNetCore.Mvc;
using SistemaInventarioTallerMecanico.Application.Contract;
using SistemaInventarioTallerMecanico.Application.Dtos.Proveedor;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedores()
        {
            return Ok(await _proveedorService.ObtenerTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetProveedor(int id)
        {
            var proveedor = await _proveedorService.ObtenerPorIdAsync(id);
            return proveedor is null ? NotFound() : Ok(proveedor);
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorDto>> CrearProveedor(ProveedorDto dto)
        {
            var creado = await _proveedorService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetProveedor), new { id = creado.Id }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, ProveedorDto dto)
        {
            var actualizado = await _proveedorService.ActualizarAsync(id, dto);
            return actualizado ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var eliminado = await _proveedorService.EliminarAsync(id);
            return eliminado ? NoContent() : NotFound();
        }
    }
}
