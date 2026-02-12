using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;
using static Prueba21.Models.Habitacion;

namespace Prueba21.Service.Implementation
{
    public class HabitacionService : IHabitacionService
    {
        private readonly ApplicationDbContext _context;

        public HabitacionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Habitacion>> GetAllAsync()
        {
            return await _context.Habitaciones.ToListAsync();
        }

        public async Task<Habitacion?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _context.Habitaciones.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Habitacion habitacion)
        {
            if (habitacion == null || habitacion.Precio <= 0 || string.IsNullOrWhiteSpace(habitacion.Numero))
                return false;

            _context.Habitaciones.Add(habitacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Habitacion habitacion)
        {
            if (id <= 0 || habitacion == null || habitacion.Precio <= 0 || string.IsNullOrWhiteSpace(habitacion.Numero))
                return false;

            var existingHabitacion = await _context.Habitaciones.FindAsync(id);
            if (existingHabitacion == null)
                return false;

            existingHabitacion.Numero = habitacion.Numero;
            existingHabitacion.Tipo = habitacion.Tipo;
            existingHabitacion.Precio = habitacion.Precio;
            existingHabitacion.Estado = habitacion.Estado;

            _context.Habitaciones.Update(existingHabitacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
                return false;

            _context.Habitaciones.Remove(habitacion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckOutAsync(int id)
        {
            var ordenHospedaje = await _context.OrdenesHospedaje.FindAsync(id);
            if (ordenHospedaje == null)
                return false;

            ordenHospedaje.FechaCheckOut = DateTime.Now;
            _context.OrdenesHospedaje.Update(ordenHospedaje);
            await _context.SaveChangesAsync();

            var habitacion = await _context.Habitaciones.FindAsync(ordenHospedaje.HabitacionId);
            if (habitacion != null)
            {
                habitacion.Estado = EstadoHabitacion.Disponible;
                _context.Habitaciones.Update(habitacion);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
