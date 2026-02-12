using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class OrdenConserjeriaService : IOrdenConserjeriaService
    {
        private readonly ApplicationDbContext _context;

        public OrdenConserjeriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenConserjeria>> ObtenerTodas()
        {
            return await _context.OrdenesConserjeria
                .Include(o => o.Habitacion)
                .Include(o => o.Personal)
                .ToListAsync();
        }

        public async Task<OrdenConserjeria> ObtenerPorId(int id)
        {
            return await _context.OrdenesConserjeria
                .Include(o => o.Habitacion)
                .Include(o => o.Personal)
                .FirstOrDefaultAsync(o => o.OrdenConserjeriaId == id);
        }

        public async Task<Dictionary<string, SelectList>> ObtenerListasDeSeleccionAsync(int? habitacionId = null, int? personalId = null)
        {
            var listas = new Dictionary<string, SelectList>
            {
                { "HabitacionId", new SelectList(await _context.Habitaciones
                    .Where(h => h.Estado == Habitacion.EstadoHabitacion.Disponible || h.Estado == Habitacion.EstadoHabitacion.EnMantenimiento)
                    .OrderBy(h => h.Numero)
                    .ToListAsync(), "HabitacionId", "Numero", habitacionId) },

                { "PersonalId", new SelectList(await _context.Personal.OrderBy(p => p.Nombre).ToListAsync(), "PersonalId", "Nombre", personalId) }
            };

            return listas;
        }

        public async Task<bool> CrearOrden(OrdenConserjeria orden)
        {
            if (orden.FechaFin <= orden.FechaInicio) return false;

            try
            {
                _context.Add(orden);
                var habitacion = await _context.Habitaciones.FindAsync(orden.HabitacionId);
                if (habitacion != null)
                {
                    habitacion.Estado = Habitacion.EstadoHabitacion.EnMantenimiento;
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

        public async Task<bool> FinalizarOrden(int id)
        {
            var orden = await _context.OrdenesConserjeria.FindAsync(id);
            if (orden == null) return false;

            orden.Estado = "Finalizado";
            _context.Update(orden);

            var habitacion = await _context.Habitaciones.FindAsync(orden.HabitacionId);
            if (habitacion != null)
            {
                habitacion.Estado = Habitacion.EstadoHabitacion.Disponible;
                _context.Update(habitacion);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarOrden(int id)
        {
            var orden = await _context.OrdenesConserjeria.FindAsync(id);
            if (orden == null) return false;

            var habitacion = await _context.Habitaciones.FindAsync(orden.HabitacionId);
            if (habitacion != null && orden.Estado != "Finalizado")
            {
                habitacion.Estado = Habitacion.EstadoHabitacion.Disponible;
                _context.Update(habitacion);
            }

            _context.OrdenesConserjeria.Remove(orden);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
