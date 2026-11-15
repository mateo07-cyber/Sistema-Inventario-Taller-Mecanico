using System.ComponentModel.DataAnnotations;

namespace SistemaInventarioTallerMecanico.Application.Dtos.Producto
{
    public class ProductoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Range(typeof(decimal), "0.01", "999999999", ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        [Range(0, 1000000, ErrorMessage = "La cantidad no puede ser negativa.")]
        public int Cantidad { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un proveedor válido.")]
        public int ProveedorId { get; set; }
    }
}
