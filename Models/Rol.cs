using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Rol
    {
        [Key]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "Máximo 255 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        // Relaciones
        public virtual ICollection<Personal> Personales { get; set; } = new List<Personal>();
    }
}