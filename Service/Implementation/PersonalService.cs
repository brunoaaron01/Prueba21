using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class PersonalService : IPersonalService
    {
        private readonly ApplicationDbContext _context;

        public PersonalService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Personal>> ObtenerTodo()
        {
            return await _context.Personal.ToListAsync();
        }

        public async Task<Personal> ObtenerPorId(int id)
        {
            return await _context.Personal.FirstOrDefaultAsync(p => p.PersonalId == id);
        }

        public async Task<bool> CrearPersonal(Personal personal)
        {
            try
            {
                _context.Personal.Add(personal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditarPersonal(Personal personal)
        {
            try
            {
                _context.Personal.Update(personal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return await _context.Personal.AnyAsync(p => p.PersonalId == personal.PersonalId);
            }
        }

        public async Task<bool> EliminarPersonal(int id)
        {
            var personal = await _context.Personal.FindAsync(id);
            if (personal == null) return false;

            _context.Personal.Remove(personal);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
