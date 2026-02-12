using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class OrdenReservaService : IOrdenReservaService
    {
        private readonly ApplicationDbContext _context;

        public OrdenReservaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenReserva>> ObtenerTodas()
        {
            return await _context.OrdenesReserva
                .Include(o => o.Cliente)
                .Include(o => o.FormaDePago)
                .Include(o => o.Habitacion)
                .ToListAsync();
        }

        public async Task<OrdenReserva> ObtenerPorId(int id)
        {
            return await _context.OrdenesReserva
                .Include(o => o.Cliente)
                .Include(o => o.FormaDePago)
                .Include(o => o.Habitacion)
                .FirstOrDefaultAsync(o => o.OrdenReservaId == id);
        }

        public async Task<bool> CrearOrden(OrdenReserva ordenReserva)
        {
            if (ordenReserva.FechaSalida <= ordenReserva.FechaEntrada) return false;

            try
            {
                _context.Add(ordenReserva);
                var habitacion = await _context.Habitaciones.FindAsync(ordenReserva.HabitacionId);
                if (habitacion != null)
                {
                    habitacion.Estado = Habitacion.EstadoHabitacion.Reservada;
                    _context.Update(habitacion);
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EliminarOrden(int id)
        {
            var ordenReserva = await _context.OrdenesReserva.FindAsync(id);
            if (ordenReserva == null) return false;

            _context.OrdenesReserva.Remove(ordenReserva);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SelectList> ObtenerClientesSelectList(int? seleccionado = null)
        {
            var clientes = await _context.Clientes.ToListAsync();
            return new SelectList(clientes, "ClienteId", "Nombre", seleccionado);
        }

        public async Task<SelectList> ObtenerFormasDePagoSelectList(int? seleccionado = null)
        {
            var formasPago = await _context.FormasDePago.ToListAsync();
            return new SelectList(formasPago, "FormaDePagoId", "Nombre", seleccionado);
        }

        public async Task<SelectList> ObtenerHabitacionesDisponiblesSelectList(int? seleccionado = null)
        {
            var habitaciones = await _context.Habitaciones
                .Where(h => h.Estado == Habitacion.EstadoHabitacion.Disponible)
                .ToListAsync();
            return new SelectList(habitaciones, "HabitacionId", "Numero", seleccionado);
        }
    }
}
