using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class ClienteService : IClienteService
    {
        private readonly ApplicationDbContext _context;

        public ClienteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del cliente debe ser mayor que 0.");

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró un cliente con ID {id}.");

            return cliente;
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Email))
                throw new ArgumentException("El email del cliente es obligatorio.");

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateClienteAsync(Cliente cliente)
        {
            if (cliente == null)
                throw new ArgumentNullException(nameof(cliente), "El cliente no puede ser nulo.");

            if (!_context.Clientes.Any(c => c.ClienteId == cliente.ClienteId))
                throw new KeyNotFoundException($"No se encontró un cliente con ID {cliente.ClienteId}.");

            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClienteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del cliente debe ser mayor que 0.");

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException($"No se encontró un cliente con ID {id}.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}