using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Encuesta
    {
        [Key]
        [Column("id_encuesta")]
        public int IdEncuesta { get; set; }

        [Required(ErrorMessage = "La orden de hospedaje es obligatoria.")]
        [Column("id_orden_hospedaje")]
        public int IdOrdenHospedaje { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La recomendación es obligatoria.")]
        [Range(1, 10, ErrorMessage = "La recomendación debe estar entre 1 y 10.")]
        [Column("recomendacion")]
        public int Recomendacion { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("lugar_origen")]
        public string? LugarOrigen { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("motivo_viaje")]
        public string? MotivoViaje { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        [Column("calificacion_limpieza")]
        public int? CalificacionLimpieza { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        [Column("calificacion_servicio")]
        public int? CalificacionServicio { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        [Column("calificacion_ubicacion")]
        public int? CalificacionUbicacion { get; set; }

        [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
        [Column("calificacion_precio")]
        public int? CalificacionPrecio { get; set; }

        [StringLength(1000, ErrorMessage = "Máximo 1000 caracteres.")]
        [Column("comentarios")]
        public string? Comentarios { get; set; }

        [Column("fecha_encuesta")]
        public DateTime FechaEncuesta { get; set; } = DateTime.UtcNow;

        // Relaciones (Foreign Keys)
        [ForeignKey("IdOrdenHospedaje")]
        public virtual OrdenHospedaje OrdenHospedaje { get; set; }
    }
}