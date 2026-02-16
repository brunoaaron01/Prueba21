using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public int IdPago { get; set; }

        [Required(ErrorMessage = "El documento es obligatorio.")]
        [Column("id_documento")]
        public int IdDocumento { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "El monto pagado es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        [Column("monto_pagado", TypeName = "decimal(12,2)")]
        public decimal MontoPagado { get; set; }

        [Required(ErrorMessage = "El método de pago es obligatorio.")]
        [Column("metodo")]
        public int Metodo { get; set; }

        [Required(ErrorMessage = "El estado del pago es obligatorio.")]
        [Column("estado_pago")]
        public int EstadoPago { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("numero_comprobante")]
        public string? NumeroComprobante { get; set; }

        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Column("numero_operacion")]
        public string? NumeroOperacion { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Column("id_personal")]
        public int? IdPersonal { get; set; }

        // Relaciones (Foreign Keys)
        [ForeignKey("IdDocumento")]
        public virtual Documento Documento { get; set; }

        [ForeignKey("Metodo")]
        public virtual MetodoPago MetodoPago { get; set; }

        [ForeignKey("EstadoPago")]
        public virtual EstadoPago EstadoPagoNavigation { get; set; }

        [ForeignKey("IdPersonal")]
        public virtual Personal? Personal { get; set; }
    }
}