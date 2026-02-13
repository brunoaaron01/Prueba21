using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class EstadoDocumento
    {
        [Key]
        [Column("id_estado_documento")]
        public int IdEstadoDocumento { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        // Relaciones
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}