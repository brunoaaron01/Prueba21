using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba21.Models
{
    public class Documento
    {
        [Key]
        [Column("id_documento")]
        public int IdDocumento { get; set; }

        [Required(ErrorMessage = "El número de documento es obligatorio.")]
        [StringLength(50, ErrorMessage = "Máximo 50 caracteres.")]
        [Column("numero_documento")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio.")]
        [Column("tipo_documento")]
        public int TipoDocumento { get; set; }

        // Referencias a las fuentes (solo una debe estar llena)
        [Column("id_reserva")]
        public int? IdReserva { get; set; }

        [Column("id_orden_hospedaje")]
        public int? IdOrdenHospedaje { get; set; }

        [Column("id_orden_conserjeria")]
        public int? IdOrdenConserjeria { get; set; }

        [Required(ErrorMessage = "El monto total es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser mayor o igual a 0.")]
        [Column("monto_total", TypeName = "decimal(12,2)")]
        public decimal MontoTotal { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "El monto debe ser mayor o igual a 0.")]
        [Column("monto_pagado", TypeName = "decimal(12,2)")]
        public decimal MontoPagado { get; set; } = 0;

        [Column("saldo_pendiente", TypeName = "decimal(12,2)")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal SaldoPendiente { get; private set; }

        [Column("fecha_emision")]
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;

        [Column("fecha_vencimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }

        [StringLength(500, ErrorMessage = "Máximo 500 caracteres.")]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El estado del documento es obligatorio.")]
        [Column("estado_documento")]
        public int EstadoDocumento { get; set; }

        // Relaciones (Foreign Keys)
        [ForeignKey("TipoDocumento")]
        public virtual TipoDocumentoCobro TipoDocumentoCobro { get; set; }

        [ForeignKey("IdReserva")]
        public virtual Reserva? Reserva { get; set; }

        [ForeignKey("IdOrdenHospedaje")]
        public virtual OrdenHospedaje? OrdenHospedaje { get; set; }

        [ForeignKey("IdOrdenConserjeria")]
        public virtual OrdenConserjeria? OrdenConserjeria { get; set; }

        [ForeignKey("EstadoDocumento")]
        public virtual EstadoDocumento EstadoDocumentoNavigation { get; set; }

        // Relaciones (Colecciones)
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}