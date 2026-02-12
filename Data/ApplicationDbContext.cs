using Microsoft.EntityFrameworkCore;
using Prueba21.Models;

namespace Prueba21.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<FormaDePago> FormasDePago { get; set; }
        public DbSet<OrdenReserva> OrdenesReserva { get; set; }
        public DbSet<OrdenHospedaje> OrdenesHospedaje { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<OrdenConserjeria> OrdenesConserjeria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración para OrdenConserjeria
            modelBuilder.Entity<OrdenConserjeria>()
                .HasOne(oc => oc.Habitacion)
                .WithMany(h => h.OrdenesConserjeria)
                .HasForeignKey(oc => oc.HabitacionId);

            modelBuilder.Entity<OrdenConserjeria>()
                .HasOne(oc => oc.Personal)
                .WithMany(p => p.OrdenesConserjeria)
                .HasForeignKey(oc => oc.PersonalId);

            // Configuración para OrdenReserva (1:N con Cliente)
            modelBuilder.Entity<OrdenReserva>()
                .HasOne(or => or.Cliente)
                .WithMany(c => c.OrdenReserva)
                .HasForeignKey(or => or.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración para FormaDePago
            modelBuilder.Entity<FormaDePago>()
                .HasMany(fp => fp.OrdenesReserva)
                .WithOne(or => or.FormaDePago)
                .HasForeignKey(or => or.FormaDePagoId);

            modelBuilder.Entity<FormaDePago>()
                .HasMany(fp => fp.OrdenesHospedaje)
                .WithOne(oh => oh.FormaDePago)
                .HasForeignKey(oh => oh.FormaDePagoId);

            // Restricción CHECK para OrdenHospedaje
            modelBuilder.Entity<OrdenHospedaje>()
                .HasCheckConstraint(
                    name: "CHK_OrdenHospedaje_Tipo",
                    sql: "(OrdenReservaId IS NOT NULL) OR (ClienteId IS NOT NULL AND HabitacionId IS NOT NULL)"
                );
            modelBuilder.Entity<Personal>().HasData(
                new Personal
                    {
                        PersonalId = 1,
                        Nombre = "Administrador",
                        Email = "admin@gmail.com",
                        Contrasenia = "admin",
                        Rol = "Administrador"
                    }
                );

        }
    }
}
