
using Microsoft.AspNetCore.Mvc;
using SistemaInventarioTallerMecanico.API.Models.Dtos;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;

namespace SistemaInventarioTallerMecanico.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IProveedorRepository _proveedorRepository;

        public ProductosController(
            IProductoRepository productoRepository,
            IProveedorRepository proveedorRepository)
        {
            _productoRepository = productoRepository;
            _proveedorRepository = proveedorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();

            var resultado = productos.Select(p => new ProductoDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Cantidad = p.Cantidad,
                ProveedorId = p.ProveedorId
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

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

        [HttpPost]
        public async Task<ActionResult<ProductoDto>> CrearProducto(ProductoDto dto)
        {
            var proveedor = await _proveedorRepository
                .ObtenerPorIdAsync(dto.ProveedorId);

            if (proveedor == null)
                return BadRequest("El proveedor indicado no existe.");

            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Cantidad = dto.Cantidad,
                ProveedorId = dto.ProveedorId
            };

            await _productoRepository.CrearAsync(producto);

            dto.Id = producto.Id;

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.Id },
                dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(
            int id, ProductoDto dto)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);

            if (producto == null)
                return NotFound();

            var proveedor = await _proveedorRepository
                .ObtenerPorIdAsync(dto.ProveedorId);

            if (proveedor == null)
                return BadRequest("El proveedor indicado no existe.");

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Cantidad = dto.Cantidad;
            producto.ProveedorId = dto.ProveedorId;

            await _productoRepository.ActualizarAsync(producto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var eliminado = await _productoRepository.EliminarAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
