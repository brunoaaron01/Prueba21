using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class Habitacion
    {
        public enum EstadoHabitacion
        {
            Disponible,
            Reservada,
            Ocupada,
            EnMantenimiento
        }

        [Key]
        public int HabitacionId { get; set; }
        [Required(ErrorMessage = "El número es obligatorio")]
        [StringLength(10, ErrorMessage = "Máximo 10 caracteres")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "El tipo es obligatorio")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Tipo { get; set; }
        
        [Required(ErrorMessage = "El precio es obligatorio")]
        [DataType(DataType.Currency)]
        [Precision(18, 2)]
        public decimal Precio { get; set; }

        [Required]
        public EstadoHabitacion Estado { get; set; } = EstadoHabitacion.Disponible;

        //Relaciones
        public ICollection<OrdenHospedaje> OrdenesHospedaje { get; set; } = new List<OrdenHospedaje>();
        public ICollection<OrdenReserva> OrdenesReserva { get; set; } = new List<OrdenReserva>();
        public ICollection<OrdenConserjeria> OrdenesConserjeria { get; set; } = new List<OrdenConserjeria>();
    }
}
