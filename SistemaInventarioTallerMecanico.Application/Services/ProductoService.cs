using SistemaInventarioTallerMecanico.Application.Contract;
using SistemaInventarioTallerMecanico.Application.Dtos.Producto;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;

namespace SistemaInventarioTallerMecanico.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IProveedorRepository _proveedorRepository;

        public ProductoService(
            IProductoRepository productoRepository,
            IProveedorRepository proveedorRepository)
        {
            _productoRepository = productoRepository;
            _proveedorRepository = proveedorRepository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();
            return productos.Select(MapearDto).ToList();
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) return null;

            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            return producto is null ? null : MapearDto(producto);
        }

        public async Task<ProductoDto?> CrearAsync(ProductoDto dto)
        {
            if (dto.Precio <= 0 || dto.Cantidad < 0 || dto.ProveedorId <= 0)
                return null;

            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(dto.ProveedorId);
            if (proveedor is null) return null;

            var producto = new Producto
            {
                Nombre = dto.Nombre.Trim(),
                Precio = dto.Precio,
                Cantidad = dto.Cantidad,
                ProveedorId = dto.ProveedorId
            };

            var creado = await _productoRepository.CrearAsync(producto);
            return MapearDto(creado);
        }

        public async Task<bool> ActualizarAsync(int id, ProductoDto dto)
        {
            if (id <= 0 || dto.Precio <= 0 || dto.Cantidad < 0 || dto.ProveedorId <= 0)
                return false;

            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto is null) return false;

            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(dto.ProveedorId);
            if (proveedor is null) return false;

            producto.Nombre = dto.Nombre.Trim();
            producto.Precio = dto.Precio;
            producto.Cantidad = dto.Cantidad;
            producto.ProveedorId = dto.ProveedorId;

            return await _productoRepository.ActualizarAsync(producto);
        }

        public Task<bool> EliminarAsync(int id)
        {
            return id <= 0
                ? Task.FromResult(false)
                : _productoRepository.EliminarAsync(id);
        }

        private static ProductoDto MapearDto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Cantidad = producto.Cantidad,
                ProveedorId = producto.ProveedorId
            };
        }
    }
}
