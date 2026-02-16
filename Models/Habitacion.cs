using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Habitacion
    {
        [Key]
        [Column("id_habitacion")]
        public int IdHabitacion { get; set; }

        [Required(ErrorMessage = "El número es obligatorio.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Column("numero")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El piso es obligatorio.")]
        [Column("piso")]
        public int Piso { get; set; }

        [Required(ErrorMessage = "El tipo de habitación es obligatorio.")]
        [Column("id_tipo_habitacion")]
        public int IdTipoHabitacion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [Column("estado")]
        public int Estado { get; set; }

        // Relaciones (Foreign Keys)
        [ForeignKey("IdTipoHabitacion")]
        public virtual TipoHabitacion TipoHabitacion { get; set; }

        [ForeignKey("Estado")]
        public virtual EstadoHabitacion EstadoHabitacion { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<OrdenConserjeria> OrdenesConserjeria { get; set; } = new List<OrdenConserjeria>();
    }
}