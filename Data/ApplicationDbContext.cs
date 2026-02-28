using Microsoft.EntityFrameworkCore;
using Prueba21.Models;

namespace Prueba21.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        // DbSets - Tablas principales
        public DbSet<Huesped> Huespedes { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<TipoHabitacion> TiposHabitacion { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<OrdenHospedaje> OrdenesHospedaje { get; set; }
        public DbSet<OrdenConserjeria> OrdenesConserjeria { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Encuesta> Encuestas { get; set; }

        // DbSets - Tablas de catálogo/lookup
        public DbSet<TipoDocumento> TiposDocumento { get; set; }
        public DbSet<TipoDocumentoCobro> TiposDocumentoCobro { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<EstadoReserva> EstadosReserva { get; set; }
        public DbSet<EstadoHabitacion> EstadosHabitacion { get; set; }
        public DbSet<EstadoOrdenHospedaje> EstadosOrdenHospedaje { get; set; }
        public DbSet<EstadoOrdenConserjeria> EstadosOrdenConserjeria { get; set; }
        public DbSet<EstadoDocumento> EstadosDocumento { get; set; }
        public DbSet<EstadoPago> EstadosPago { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ========== MAPEO EXPLÍCITO DE NOMBRES DE TABLAS ==========
            // CRÍTICO: Sin estos mapeos, EF busca nombres plurales que no existen en SQL

            // Tablas principales
            modelBuilder.Entity<Huesped>().ToTable("Huesped");
            modelBuilder.Entity<Personal>().ToTable("Personal");
            modelBuilder.Entity<Habitacion>().ToTable("Habitacion");
            modelBuilder.Entity<TipoHabitacion>().ToTable("TipoHabitacion");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<OrdenHospedaje>().ToTable("OrdenHospedaje");
            modelBuilder.Entity<OrdenConserjeria>().ToTable("OrdenConserjeria");
            modelBuilder.Entity<Documento>().ToTable("Documento");
            modelBuilder.Entity<Pago>().ToTable("Pago");
            modelBuilder.Entity<Encuesta>().ToTable("Encuesta");

            // Tablas de catálogo (ESTOS SON LOS QUE FALTABAN)
            modelBuilder.Entity<TipoDocumento>().ToTable("TipoDocumento");
            modelBuilder.Entity<TipoDocumentoCobro>().ToTable("TipoDocumentoCobro");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<EstadoReserva>().ToTable("EstadoReserva");
            modelBuilder.Entity<EstadoHabitacion>().ToTable("EstadoHabitacion");
            modelBuilder.Entity<EstadoOrdenHospedaje>().ToTable("EstadoOrdenHospedaje");
            modelBuilder.Entity<EstadoOrdenConserjeria>().ToTable("EstadoOrdenConserjeria");
            modelBuilder.Entity<EstadoDocumento>().ToTable("EstadoDocumento");
            modelBuilder.Entity<EstadoPago>().ToTable("EstadoPago");
            modelBuilder.Entity<MetodoPago>().ToTable("MetodoPago");

            // ========== ÍNDICES ÚNICOS ==========

            // Huesped - Constraint único en tipo y número de documento
            modelBuilder.Entity<Huesped>()
                .HasIndex(h => new { h.TipoDocumento, h.NumDocumento })
                .IsUnique()
                .HasDatabaseName("UQ_Huesped_Documento");

            // Personal - Constraint único en tipo y número de documento
            modelBuilder.Entity<Personal>()
                .HasIndex(p => new { p.TipoDocumento, p.NumDocumento })
                .IsUnique()
                .HasDatabaseName("UQ_Personal_Documento");

            // Habitacion - Constraint único en número y piso
            modelBuilder.Entity<Habitacion>()
                .HasIndex(h => new { h.Numero, h.Piso })
                .IsUnique()
                .HasDatabaseName("UQ_Habitacion_Numero_Piso");

            // Documento - Constraint único en número de documento
            modelBuilder.Entity<Documento>()
                .HasIndex(d => d.NumeroDocumento)
                .IsUnique();

            // Encuesta - Relación 1:1 con OrdenHospedaje
            modelBuilder.Entity<Encuesta>()
                .HasIndex(e => e.IdOrdenHospedaje)
                .IsUnique()
                .HasDatabaseName("UQ_Encuesta_OrdenHospedaje");

            // ========== RELACIONES ==========

            // Reserva -> Huesped
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Huesped)
                .WithMany(h => h.Reservas)
                .HasForeignKey(r => r.IdHuesped)
                .OnDelete(DeleteBehavior.Restrict);

            // Reserva -> Habitacion
            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Habitacion)
                .WithMany(h => h.Reservas)
                .HasForeignKey(r => r.IdHabitacion)
                .OnDelete(DeleteBehavior.Restrict);

            // OrdenHospedaje -> Reserva
            modelBuilder.Entity<OrdenHospedaje>()
                .HasOne(oh => oh.Reserva)
                .WithMany(r => r.OrdenesHospedaje)
                .HasForeignKey(oh => oh.IdReserva)
                .OnDelete(DeleteBehavior.Restrict);

            // OrdenConserjeria -> Personal
            modelBuilder.Entity<OrdenConserjeria>()
                .HasOne(oc => oc.Personal)
                .WithMany(p => p.OrdenesConserjeria)
                .HasForeignKey(oc => oc.IdPersonal)
                .OnDelete(DeleteBehavior.Restrict);

            // OrdenConserjeria -> Habitacion
            modelBuilder.Entity<OrdenConserjeria>()
                .HasOne(oc => oc.Habitacion)
                .WithMany(h => h.OrdenesConserjeria)
                .HasForeignKey(oc => oc.IdHabitacion)
                .OnDelete(DeleteBehavior.Restrict);

            // OrdenConserjeria -> Reserva (opcional)
            modelBuilder.Entity<OrdenConserjeria>()
                .HasOne(oc => oc.Reserva)
                .WithMany(r => r.OrdenesConserjeria)
                .HasForeignKey(oc => oc.IdReserva)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Pago -> Documento
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Documento)
                .WithMany(d => d.Pagos)
                .HasForeignKey(p => p.IdDocumento)
                .OnDelete(DeleteBehavior.Restrict);

            // Pago -> Personal (opcional)
            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Personal)
                .WithMany(per => per.Pagos)
                .HasForeignKey(p => p.IdPersonal)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

            // Encuesta -> OrdenHospedaje (1:1)
            modelBuilder.Entity<Encuesta>()
                .HasOne(e => e.OrdenHospedaje)
                .WithOne(oh => oh.Encuesta)
                .HasForeignKey<Encuesta>(e => e.IdOrdenHospedaje)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}