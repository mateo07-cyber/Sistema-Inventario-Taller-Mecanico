
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.API.Data;
using SistemaInventarioTallerMecanico.API.Models.Entities;
using SistemaInventarioTallerMecanico.API.Models.Dtos;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        // Obtener todos los productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Cantidad = p.Cantidad,
                    ProveedorId = p.ProveedorId
                })
                .ToListAsync();

            return Ok(productos);
        }

        // Obtener un producto por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            return Ok(new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Cantidad = producto.Cantidad,
                ProveedorId = producto.ProveedorId
            });
        }

        // Crear un producto
        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto(ProductoDto dto)
        {
            var proveedorExiste = await _context.Proveedores
                .AnyAsync(p => p.Id == dto.ProveedorId);

            if (!proveedorExiste)
                return BadRequest("El proveedor indicado no existe.");

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Cantidad = dto.Cantidad,
                ProveedorId = dto.ProveedorId
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            dto.Id = producto.Id;

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.Id },
                dto);
        }

        // Actualizar un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(
            int id, ProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            var proveedorExiste = await _context.Proveedores
                .AnyAsync(p => p.Id == dto.ProveedorId);

            if (!proveedorExiste)
                return BadRequest("El proveedor indicado no existe.");

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Cantidad = dto.Cantidad;
            producto.ProveedorId = dto.ProveedorId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Eliminar un producto
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
