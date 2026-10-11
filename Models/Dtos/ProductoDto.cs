
namespace SistemaInventarioTallerMecanico.API.Models.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public int Cantidad { get; set; }

        public int ProveedorId { get; set; }
    }
}
