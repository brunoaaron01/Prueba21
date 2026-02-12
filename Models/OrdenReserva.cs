using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class OrdenReserva
    {
        [Key]
        public int OrdenReservaId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int HabitacionId { get; set; }

        [Required]
        public int FormaDePagoId { get; set; }

        [Required(ErrorMessage = "La fecha de entrada es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrada { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaSalida { get; set; }

        // 🔹 Hacer las propiedades de navegación opcionales (nullables)
        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }

        [ForeignKey("HabitacionId")]
        public virtual Habitacion? Habitacion { get; set; }

        [ForeignKey("FormaDePagoId")]
        public virtual FormaDePago? FormaDePago { get; set; }
    }
}
