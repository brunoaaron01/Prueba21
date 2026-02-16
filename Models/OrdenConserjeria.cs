using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class OrdenConserjeria
    {
        [Key]
        [Column("id_orden_conserj")]
        public int IdOrdenConserj { get; set; }

        [Required(ErrorMessage = "El personal es obligatorio.")]
        [Column("id_personal")]
        public int IdPersonal { get; set; }

        [Required(ErrorMessage = "La habitación es obligatoria.")]
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }

        [Column("id_reserva")]
        public int? IdReserva { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [Column("fecha_inicio")]
        public DateTime FechaInicio { get; set; }

        [Column("fecha_fin")]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser mayor o igual a 0.")]
        [Column("precio", TypeName = "decimal(12,2)")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Column("estado")]
        public int Estado { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        // Relaciones (Foreign Keys)
        [ForeignKey("IdPersonal")]
        public virtual Personal Personal { get; set; }

        [ForeignKey("IdHabitacion")]
        public virtual Habitacion Habitacion { get; set; }

        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }

        [ForeignKey("Estado")]
        public virtual EstadoOrdenConserjeria EstadoOrdenConserjeria { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}