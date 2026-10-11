
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.API.Data;
using SistemaInventarioTallerMecanico.API.Models.Entities;
using SistemaInventarioTallerMecanico.API.Models.Dtos;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly DataContext _context;

        public ProveedoresController(DataContext context)
        {
            _context = context;
        }

        // Obtener todos los proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDto>>> GetProveedores()
        {
            var proveedores = await _context.Proveedores
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Correo = p.Correo
                })
                .ToListAsync();

            return Ok(proveedores);
        }

        // Obtener un proveedor por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

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

        // Crear un proveedor
        [HttpPost]
        public async Task<ActionResult<ProveedorDto>> CrearProveedor(ProveedorDto dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            dto.Id = proveedor.Id;

            return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.Id }, dto);
        }

        // Actualizar un proveedor
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProveedor(int id, ProveedorDto dto)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            proveedor.Nombre = dto.Nombre;
            proveedor.Telefono = dto.Telefono;
            proveedor.Correo = dto.Correo;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un proveedor
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
