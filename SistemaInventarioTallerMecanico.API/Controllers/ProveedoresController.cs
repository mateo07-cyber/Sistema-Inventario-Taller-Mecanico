
using Microsoft.AspNetCore.Mvc;
using SistemaInventarioTallerMecanico.API.Models.Dtos;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedoresController(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedores()
        {
            var proveedores = await _proveedorRepository.ObtenerTodosAsync();

            var resultado = proveedores.Select(p => new ProveedorDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetProveedor(int id)
        {
            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(id);

            if (proveedor == null)
                return NotFound();

            return Ok(new ProveedorDto
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Correo = proveedor.Correo
            });
        }

        [HttpPost]
        public async Task<ActionResult<ProveedorDto>> CrearProveedor(ProveedorDto dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            await _proveedorRepository.CrearAsync(proveedor);

            dto.Id = proveedor.Id;

            return CreatedAtAction(
                nameof(GetProveedor),
                new { id = proveedor.Id },
                dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(
            int id, ProveedorDto dto)
        {
            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(id);

            if (proveedor == null)
                return NotFound();

            proveedor.Nombre = dto.Nombre;
            proveedor.Telefono = dto.Telefono;
            proveedor.Correo = dto.Correo;

            await _proveedorRepository.ActualizarAsync(proveedor);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var eliminado = await _proveedorRepository.EliminarAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
