using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ApplicationDbContext _context;

        public TipoDocumentoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TipoDocumento>> ObtenerTodo()
        {
            return await _context.TiposDocumento.ToListAsync();
        }

        public async Task<TipoDocumento> ObtenerPorId(int id)
        {
            return await _context.TiposDocumento.FindAsync(id);
        }
    }
}