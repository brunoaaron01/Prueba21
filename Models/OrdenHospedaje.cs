using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Prueba21.Models
{
    public class OrdenHospedaje
    {
        [Key]
        public int OrdenHospedajeId { get; set; }

        // Campos para reserva previa (opcional)
        public int? OrdenReservaId { get; set; }

        // Cliente asociado (obligatorio si no hay reserva)
        public int? ClienteId { get; set; }
        public int? HabitacionId { get; set; }

        // 🔹 Eliminamos la relación directa con `Habitacion`
        // public int? HabitacionId { get; set; }  ❌ ELIMINADO

        [Required(ErrorMessage = "La Forma de Pago es Obligatoria")]
        public int FormaDePagoId { get; set; }

        [Required(ErrorMessage = "La fecha de check-in es obligatoria")]
        [DataType(DataType.DateTime)]
        public DateTime FechaCheckIn { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        [CustomValidation(typeof(OrdenHospedaje), nameof(ValidateFechaCheckOut))]
        public DateTime? FechaCheckOut { get; set; }

        [Required]
        public string Estado { get; set; } = "En curso"; // Estado inicial


        // Relaciones
        [ForeignKey("OrdenReservaId")]
        public OrdenReserva? OrdenReserva { get; set; }

        [ForeignKey("HabitacionId")]
        public Habitacion? Habitacion { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [ForeignKey("FormaDePagoId")]
        public FormaDePago? FormaDePago { get; set; }

        public static ValidationResult ValidateFechaCheckOut(DateTime? fechaCheckOut, ValidationContext context)
        {
            var ordenHospedaje = context.ObjectInstance as OrdenHospedaje;
            if (fechaCheckOut.HasValue && ordenHospedaje.FechaCheckIn > fechaCheckOut.Value)
            {
                return new ValidationResult("La fecha de check-out debe ser mayor a la fecha de check-in");
            }
            return ValidationResult.Success;
        }
    }
}
