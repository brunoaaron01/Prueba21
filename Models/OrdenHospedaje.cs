using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class OrdenHospedaje
    {
        [Key]
        [Column("id_orden_hospedaje")]
        public int IdOrdenHospedaje { get; set; }

        [Required(ErrorMessage = "La reserva es obligatoria.")]
        [Column("id_reserva")]
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Column("estado")]
        public int Estado { get; set; }

        [Column("fecha_checkin")]
        public DateTime? FechaCheckin { get; set; }

        [Column("fecha_checkout")]
        public DateTime? FechaCheckout { get; set; }

        // Relaciones (Foreign Keys)
        [ForeignKey("IdReserva")]
        public virtual Reserva Reserva { get; set; }

        [ForeignKey("Estado")]
        public virtual EstadoOrdenHospedaje EstadoOrdenHospedaje { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
        public virtual Encuesta? Encuesta { get; set; }
    }
}