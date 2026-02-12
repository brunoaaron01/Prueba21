using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Prueba21.Models
{
    public class OrdenConserjeria
    {
        [Key]
        public int OrdenConserjeriaId { get; set; }

        [Required(ErrorMessage = "La habitación es obligatoria.")]
        public int HabitacionId { get; set; }

        [Required(ErrorMessage = "El personal es obligatorio.")]
        public int PersonalId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaFin { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        public string Descripcion { get; set; }

        // ✅ Agregar propiedad Estado
        [Required]
        public string Estado { get; set; } = "En Proceso"; 

        [ForeignKey("HabitacionId")]
        public  Habitacion? Habitacion { get; set; }

        [ForeignKey("PersonalId")]
        public  Personal? Personal { get; set; }
    }
}
