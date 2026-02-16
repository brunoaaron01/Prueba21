using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Huesped
    {
        [Key]
        [Column("id_huesped")]
        public int IdHuesped { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("nombres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("apellidos")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [Column("tipo_documento")]
        public int TipoDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("num_documento")]
        public string NumDocumento { get; set; }

        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Phone(ErrorMessage = "Formato de teléfono inválido.")]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        [StringLength(255, ErrorMessage = "Máximo 255 caracteres.")]
        [Column("correo")]
        public string? Correo { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Relaciones (Foreign Keys)
        [ForeignKey("TipoDocumento")]
        public virtual TipoDocumento TipoDocumentoNavigation { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }//comentario de prueba
}