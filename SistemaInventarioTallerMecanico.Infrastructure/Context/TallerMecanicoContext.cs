
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.Domain.Entities;

namespace SistemaInventarioTallerMecanico.Infrastructure.Context
{
    public class TallerMecanicoContext : DbContext
    {
        public TallerMecanicoContext(
            DbContextOptions<TallerMecanicoContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Proveedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(p => p.ProveedorId);

            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);
        }
    }
}
