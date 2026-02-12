using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba21.Data;
using Prueba21.Models;
using static Prueba21.Models.Habitacion;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class OrdenHospedajeService : IOrdenHospedajeService
    {
        private readonly ApplicationDbContext _context;

        public OrdenHospedajeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenHospedaje>> ObtenerOrdenesHospedajeAsync()
        {
            return await _context.OrdenesHospedaje
                .Include(o => o.Cliente)
                .Include(o => o.FormaDePago)
                .Include(o => o.OrdenReserva)
                .Include(o => o.Habitacion)
                .ToListAsync();
        }

        public async Task<OrdenHospedaje?> ObtenerOrdenHospedajePorIdAsync(int id)
        {
            return await _context.OrdenesHospedaje
                .Include(o => o.Cliente)
                .Include(o => o.FormaDePago)
                .Include(o => o.OrdenReserva)
                .Include(o => o.Habitacion)
                .FirstOrDefaultAsync(o => o.OrdenHospedajeId == id);
        }

        public async Task<Dictionary<string, SelectList>> ObtenerListasDeSeleccionAsync(int? clienteId = null, int? formaDePagoId = null, int? habitacionId = null, int? ordenReservaId = null)
        {
            var listas = new Dictionary<string, SelectList>
            {
                { "ClienteId", new SelectList(await _context.Clientes.ToListAsync(), "ClienteId", "Nombre", clienteId) },
                { "FormaDePagoId", new SelectList(await _context.FormasDePago.ToListAsync(), "FormaDePagoId", "Nombre", formaDePagoId) },
                { "HabitacionesDisponibles", new SelectList(await _context.Habitaciones
                    .Where(h => h.Estado == EstadoHabitacion.Disponible || h.Estado == EstadoHabitacion.Reservada)
                    .OrderBy(h => h.Numero)
                    .ToListAsync(), "HabitacionId", "Numero", habitacionId) },
                { "OrdenReservaId", new SelectList(await _context.OrdenesReserva.ToListAsync(), "OrdenReservaId", "OrdenReservaId", ordenReservaId) }
            };

            return listas;
        }

        public async Task<bool> CrearOrdenHospedajeAsync(OrdenHospedaje ordenHospedaje)
        {
            try
            {
                var habitacion = await _context.Habitaciones.FindAsync(ordenHospedaje.HabitacionId);
                if (habitacion == null || (habitacion.Estado != EstadoHabitacion.Disponible && habitacion.Estado != EstadoHabitacion.Reservada))
                {
                    return false;
                }

                ordenHospedaje.Cliente = null;
                ordenHospedaje.FormaDePago = null;
                _context.Add(ordenHospedaje);
                await _context.SaveChangesAsync();

                habitacion.Estado = EstadoHabitacion.Ocupada;
                _context.Habitaciones.Update(habitacion);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> FinalizarHospedajeAsync(int id)
        {
            var ordenHospedaje = await _context.OrdenesHospedaje.FindAsync(id);
            if (ordenHospedaje == null) return false;

            ordenHospedaje.Estado = "Finalizado";
            _context.Update(ordenHospedaje);

            var habitacion = await _context.Habitaciones.FindAsync(ordenHospedaje.HabitacionId);
            if (habitacion != null)
            {
                habitacion.Estado = EstadoHabitacion.Disponible;
                _context.Update(habitacion);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarOrdenHospedajeAsync(int id)
        {
            var ordenHospedaje = await _context.OrdenesHospedaje.Include(o => o.Habitacion).FirstOrDefaultAsync(o => o.OrdenHospedajeId == id);
            if (ordenHospedaje == null) return false;

            if (ordenHospedaje.Habitacion != null)
            {
                ordenHospedaje.Habitacion.Estado = EstadoHabitacion.Disponible;
                _context.Update(ordenHospedaje.Habitacion);
            }

            _context.OrdenesHospedaje.Remove(ordenHospedaje);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}