
using SistemaInventarioTallerMecanico.Domain.Entities;

namespace SistemaInventarioTallerMecanico.Domain.Repository
{
    public interface IProveedorRepository
    {
        Task<IEnumerable<Proveedor>> ObtenerTodosAsync();

        Task<Proveedor?> ObtenerPorIdAsync(int id);

        Task<Proveedor> CrearAsync(Proveedor proveedor);

        Task<bool> ActualizarAsync(Proveedor proveedor);

        Task<bool> EliminarAsync(int id);
    }
}
