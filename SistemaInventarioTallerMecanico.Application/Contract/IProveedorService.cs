using SistemaInventarioTallerMecanico.Application.Dtos.Proveedor;

namespace SistemaInventarioTallerMecanico.Application.Contract
{
    public interface IProveedorService
    {
        Task<IEnumerable<ProveedorDto>> ObtenerTodosAsync();
        Task<ProveedorDto?> ObtenerPorIdAsync(int id);
        Task<ProveedorDto> CrearAsync(ProveedorDto dto);
        Task<bool> ActualizarAsync(int id, ProveedorDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
