using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class Personal
    {


        [Key]
        public int PersonalId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public String Contrasenia { get; set; }

        [Required(ErrorMessage = "El rol es obligatoria.")]
        public String Rol { get; set; }


        // Relaciones
        public ICollection<OrdenConserjeria> OrdenesConserjeria { get; set; } = new List<OrdenConserjeria>();
    }
}
