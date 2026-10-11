
namespace SistemaInventarioTallerMecanico.API.Models.Entities
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public int ProveedorId { get; set; }

        public Proveedor? Proveedor { get; set; }
    }
}
