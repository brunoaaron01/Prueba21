using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class TipoHabitacion
    {
        [Key]
        [Column("id_tipo_habitacion")]
        public int IdTipoHabitacion { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("nombre")]
        public string Nombre { get; set; }

        [StringLength(255, ErrorMessage = "Máximo 255 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "La capacidad es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La capacidad debe ser mayor a 0.")]
        [Column("capacidad_personas")]
        public int CapacidadPersonas { get; set; }

        [Required(ErrorMessage = "La tarifa base es obligatoria.")]
        [Range(0, double.MaxValue, ErrorMessage = "La tarifa debe ser mayor o igual a 0.")]
        [Column("tarifa_base", TypeName = "decimal(12,2)")]
        public decimal TarifaBase { get; set; }

        // Relaciones
        public virtual ICollection<Habitacion> Habitaciones { get; set; } = new List<Habitacion>();
    }
}