using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class RolService : IRolService
    {
        private readonly ApplicationDbContext _context;

        public RolService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ObtenerTodo()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> ObtenerPorId(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
    }
}