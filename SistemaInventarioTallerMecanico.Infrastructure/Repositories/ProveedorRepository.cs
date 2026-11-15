
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.Domain.Entities;
using SistemaInventarioTallerMecanico.Domain.Repository;
using SistemaInventarioTallerMecanico.Infrastructure.Context;

namespace SistemaInventarioTallerMecanico.Infrastructure.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly TallerMecanicoContext _context;

        public ProveedorRepository(TallerMecanicoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerTodosAsync()
        {
            return await _context.Proveedores
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Proveedor?> ObtenerPorIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        public async Task<Proveedor> CrearAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return proveedor;
        }

        public async Task<bool> ActualizarAsync(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return false;

            _context.Proveedores.Remove(proveedor);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
