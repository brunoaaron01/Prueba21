using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(15, ErrorMessage = "Máximo 50 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "El teléfono debe tener entre 9 y 15 caracteres.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        public string Telefono { get; set; }


        //Relaciones
        public ICollection<OrdenReserva> OrdenReserva { get; set; } = new List<OrdenReserva>();// 1:1 (Si hace reserva) 
        public ICollection<OrdenHospedaje> OrdenesHospedaje { get; set; } = new List<OrdenHospedaje>(); // 1:N (Si se hospeda)


    }
}
