using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class FormaDePago
    {
        [Key]
        public int FormaDePagoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(20, ErrorMessage = "Máximo 10 caracteres.")]
        public string Nombre { get; set; }

        // Relaciones
        public ICollection<OrdenReserva> OrdenesReserva { get; set; } = new List<OrdenReserva>();
        public ICollection<OrdenHospedaje> OrdenesHospedaje { get; set; } = new List<OrdenHospedaje>();
    }
}
