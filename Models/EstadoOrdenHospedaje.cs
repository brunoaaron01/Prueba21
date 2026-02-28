using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class EstadoOrdenHospedaje
    {
        [Key]
        [Column("id_estado_orden_hosp")]
        public int IdEstadoOrdenHosp { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        // Relaciones
        public virtual ICollection<OrdenHospedaje> OrdenesHospedaje { get; set; } = new List<OrdenHospedaje>();
    }
}