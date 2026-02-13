using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class EstadoHabitacion
    {
        [Key]
        [Column("id_estado_habitacion")]
        public int IdEstadoHabitacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        // Relaciones
        public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}