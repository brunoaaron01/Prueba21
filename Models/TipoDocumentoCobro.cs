using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class TipoDocumentoCobro
    {
        [Key]
        [Column("id_tipo_doc_cobro")]
        public int IdTipoDocCobro { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "Máximo 255 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        // Relaciones
        public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();
    }
}