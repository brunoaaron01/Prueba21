using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Reserva
    {
        [Key]
        [Column("id_reserva")]
        public int IdReserva { get; set; }

        [Required(ErrorMessage = "El huésped es obligatorio.")]
        [Column("id_huesped")]
        public int IdHuesped { get; set; }

        [Required(ErrorMessage = "La habitación es obligatoria.")]
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        [Column("fecha_entrada")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [Column("fecha_salida")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        [Required(ErrorMessage = "El número de personas es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe haber al menos 1 persona.")]
        [Column("num_personas")]
        public int NumPersonas { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser mayor o igual a 0.")]
        [Column("monto_total", TypeName = "decimal(12,2)")]
        public decimal MontoTotal { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Column("estado")]
        public int Estado { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relaciones (Foreign Keys)
        [ForeignKey("IdHuesped")]
        public virtual Huesped Huesped { get; set; }

        [ForeignKey("IdHabitacion")]
        public virtual Habitacion Habitacion { get; set; }

        [ForeignKey("Estado")]
        public virtual EstadoReserva EstadoReserva { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<OrdenHospedaje> OrdenesHospedaje { get; set; } = new List<OrdenHospedaje>();
        public virtual ICollection<OrdenConserjeria> OrdenesConserjeria { get; set; } = new List<OrdenConserjeria>();
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}