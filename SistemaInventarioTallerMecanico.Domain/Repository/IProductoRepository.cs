
using SistemaInventarioTallerMecanico.Domain.Entities;

namespace SistemaInventarioTallerMecanico.Domain.Repository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();

        Task<Producto?> ObtenerPorIdAsync(int id);

        Task<Producto> CrearAsync(Producto producto);

        Task<bool> ActualizarAsync(Producto producto);

        Task<bool> EliminarAsync(int id);
    }
}
