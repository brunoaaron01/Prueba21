using Microsoft.EntityFrameworkCore;
using Prueba21.Data;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(Personal? usuario, string? error)> ValidarUsuario(string email, string contrasenia)
        {
            // Primero buscamos por email
            var usuario = await _context.Personal.FirstOrDefaultAsync(x => x.Email == email);

            // Caso 1: Usuario no encontrado
            if (usuario == null)
                return (null, "USER_NOT_FOUND");

            // Caso 2: Contraseña incorrecta
            if (usuario.Contrasenia != contrasenia)
                return (null, "INVALID_PASSWORD");

            // Caso 3: Credenciales válidas
            return (usuario, null);
        }
    }
}