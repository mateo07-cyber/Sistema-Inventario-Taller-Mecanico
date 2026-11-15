
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;
using SistemaInventarioTallerMecanico.Infrastructure.Context;

namespace SistemaInventarioTallerMecanico.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly TallerMecanicoContext _context;

        public ProductoRepository(TallerMecanicoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<bool> ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
                return false;

            _context.Productos.Remove(producto);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
