using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class FormaDePagoService : IFormaDePagoService
    {
        private readonly ApplicationDbContext _context;

        public FormaDePagoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FormaDePago>> GetAllAsync()
        {
            return await _context.FormasDePago.ToListAsync();
        }

        public async Task<FormaDePago?> GetByIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _context.FormasDePago.FirstOrDefaultAsync(f => f.FormaDePagoId == id);
        }

        public async Task<bool> CreateAsync(FormaDePago formaDePago)
        {
            if (formaDePago == null || string.IsNullOrWhiteSpace(formaDePago.Nombre))
                return false;

            _context.FormasDePago.Add(formaDePago);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, FormaDePago formaDePago)
        {
            if (id <= 0 || formaDePago == null || string.IsNullOrWhiteSpace(formaDePago.Nombre))
                return false;

            var existingFormaDePago = await _context.FormasDePago.FindAsync(id);
            if (existingFormaDePago == null)
                return false;

            existingFormaDePago.Nombre = formaDePago.Nombre;
            _context.FormasDePago.Update(existingFormaDePago);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            var formaDePago = await _context.FormasDePago.FindAsync(id);
            if (formaDePago == null)
                return false;

            _context.FormasDePago.Remove(formaDePago);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}