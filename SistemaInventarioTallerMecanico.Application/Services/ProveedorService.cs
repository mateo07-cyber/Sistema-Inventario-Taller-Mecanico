using SistemaInventarioTallerMecanico.Application.Contract;
using SistemaInventarioTallerMecanico.Application.Dtos.Proveedor;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;

namespace SistemaInventarioTallerMecanico.Application.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync()
        {
            var proveedores = await _proveedorRepository.ObtenerTodosAsync();
            return proveedores.Select(MapearDto).ToList();
        }

        public async Task<ProveedorDto?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) return null;

            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(id);
            return proveedor is null ? null : MapearDto(proveedor);
        }

        public async Task<ProveedorDto> CrearAsync(ProveedorDto dto)
        {
            var proveedor = new Proveedor
            {
                Nombre = dto.Nombre.Trim(),
                Telefono = dto.Telefono.Trim(),
                Correo = dto.Correo.Trim().ToLowerInvariant()
            };

            var creado = await _proveedorRepository.CrearAsync(proveedor);
            return MapearDto(creado);
        }

        public async Task<bool> ActualizarAsync(int id, ProveedorDto dto)
        {
            if (id <= 0) return false;

            var proveedor = await _proveedorRepository.ObtenerPorIdAsync(id);
            if (proveedor is null) return false;

            proveedor.Nombre = dto.Nombre.Trim();
            proveedor.Telefono = dto.Telefono.Trim();
            proveedor.Correo = dto.Correo.Trim().ToLowerInvariant();

            return await _proveedorRepository.ActualizarAsync(proveedor);
        }

        public Task<bool> EliminarAsync(int id)
        {
            return id <= 0
                ? Task.FromResult(false)
                : _proveedorRepository.EliminarAsync(id);
        }

        private static ProveedorDto MapearDto(Proveedor proveedor)
        {
            return new ProveedorDto
            {
                Id = proveedor.Id,
                Nombre = proveedor.Nombre,
                Telefono = proveedor.Telefono,
                Correo = proveedor.Correo
            };
        }
    }
}
