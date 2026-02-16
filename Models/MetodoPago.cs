using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class MetodoPago
    {
        [Key]
        [Column("id_metodo_pago")]
        public int IdMetodoPago { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        // Relaciones
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}