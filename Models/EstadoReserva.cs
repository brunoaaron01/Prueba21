using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class EstadoReserva
    {
        [Key]
        [Column("id_estado_reserva")]
        public int IdEstadoReserva { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        // Relaciones
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}