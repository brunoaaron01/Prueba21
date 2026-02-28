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
            return await _context.Personal
                .Include(p => p.RolNavigation)
                .Include(p => p.TipoDocumentoNavigation)
                .ToListAsync();
        }

        public async Task<Personal> ObtenerPorId(int id)
        {
            return await _context.Personal
                .Include(p => p.RolNavigation)
                .Include(p => p.TipoDocumentoNavigation)
                .FirstOrDefaultAsync(p => p.IdPersonal == id);
        }

        public async Task<bool> CrearPersonal(Personal personal, string contrasenia)
        {
            try
            {
                // Hashear la contraseña antes de guardar
                personal.PasswordHash = HashearContrasenia(contrasenia);
                personal.FechaCreacion = DateTime.UtcNow;

                _context.Personal.Add(personal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditarPersonal(Personal personal, string? contrasenia = null)
        {
            try
            {
                var personalExistente = await _context.Personal
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.IdPersonal == personal.IdPersonal);

                if (personalExistente == null)
                    return false;

                // Si se proporciona una nueva contraseña, hashearla
                if (!string.IsNullOrWhiteSpace(contrasenia))
                {
                    personal.PasswordHash = HashearContrasenia(contrasenia);
                }
                else
                {
                    // Mantener la contraseña existente si no se proporciona una nueva
                    personal.PasswordHash = personalExistente.PasswordHash;
                }

                // Mantener la fecha de creación original
                personal.FechaCreacion = personalExistente.FechaCreacion;

                _context.Personal.Update(personal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return await _context.Personal.AnyAsync(p => p.IdPersonal == personal.IdPersonal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EliminarPersonal(int id)
        {
            try
            {
                var personal = await _context.Personal.FindAsync(id);
                if (personal == null) return false;

                _context.Personal.Remove(personal);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método auxiliar para hashear contraseñas
        private byte[] HashearContrasenia(string contrasenia)
        {
            // Opción 1: Con BCrypt (RECOMENDADO para producción)
            // string hash = BCrypt.Net.BCrypt.HashPassword(contrasenia);
            // return System.Text.Encoding.UTF8.GetBytes(hash);

            // Opción 2: Sin hashear (SOLO PARA DESARROLLO)
            return System.Text.Encoding.UTF8.GetBytes(contrasenia);
        }
    }
}