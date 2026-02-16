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
            // Buscar el personal por email e incluir la relación con Rol
            var personal = await _context.Personal
                .Include(p => p.RolNavigation)
                .Include(p => p.TipoDocumentoNavigation)
                .FirstOrDefaultAsync(x => x.Email == email);

            // Caso 1: Usuario no encontrado
            if (personal == null)
                return (null, "USER_NOT_FOUND");

            // Caso 2: Usuario inactivo
            if (!personal.Activo)
                return (null, "USER_INACTIVE");

            // Caso 3: Contraseña incorrecta
            if (!VerificarContrasenia(contrasenia, personal.PasswordHash))
                return (null, "INVALID_PASSWORD");

            // Caso 4: Credenciales válidas
            return (personal, null);
        }

        private bool VerificarContrasenia(string contrasenia, byte[] passwordHash)
        {
            string storedPassword = System.Text.Encoding.UTF8.GetString(passwordHash);
            return storedPassword == contrasenia;
        }
    }
}