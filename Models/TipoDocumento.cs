using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class TipoDocumento
    {
        [Key]
        [Column("id_tipo_documento")]
        public int IdTipoDocumento { get; set; }

        [Required(ErrorMessage = "El código es obligatorio.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Column("codigo")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("descripcion")]
        public string Descripcion { get; set; }

        // Relaciones
        public virtual ICollection<Huesped> Huespedes { get; set; } = new List<Huesped>();
        public virtual ICollection<Personal> Personales { get; set; } = new List<Personal>();
    }
}