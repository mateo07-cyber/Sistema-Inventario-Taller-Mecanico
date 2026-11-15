using SistemaInventarioTallerMecanico.Application.Dtos.Producto;

namespace SistemaInventarioTallerMecanico.Application.Contract
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerTodosAsync();
        Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<ProductoDto?> CrearAsync(ProductoDto dto);
        Task<bool> ActualizarAsync(int id, ProductoDto dto);
        Task<bool> EliminarAsync(int id);
    }
}
