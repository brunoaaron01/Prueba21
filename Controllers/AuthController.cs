using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Prueba21.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string contrasenia, bool rememberPassword = false)
        {
            // Verificar bloqueo existente
            var attempts = HttpContext.Session.GetInt32("LoginAttempts") ?? 0;
            var blockTimeString = HttpContext.Session.GetString("BlockTime");

            if (!string.IsNullOrEmpty(blockTimeString))
            {
                var blockTime = DateTime.Parse(blockTimeString);

                if (DateTime.Now < blockTime)
                {
                    var remainingSeconds = (int)(blockTime - DateTime.Now).TotalSeconds;
                    TempData["IsBlocked"] = true;
                    TempData["BlockSeconds"] = remainingSeconds;
                    TempData["ErrorMessage"] = $"Demasiados intentos. Espere {remainingSeconds} segundos antes de reintentar.";
                    return View();
                }
                else
                {
                    HttpContext.Session.Remove("LoginAttempts");
                    HttpContext.Session.Remove("BlockTime");
                    attempts = 0;
                }
            }
            // Validar usuario solo si no hay bloqueo activo
            var (usuario, error) = await _authService.ValidarUsuario(email, contrasenia);

            if (error != null)
            {
                // Incrementar contador de intentos fallidos
                attempts++;
                HttpContext.Session.SetInt32("LoginAttempts", attempts);

                // Configurar bloqueo después de 3 intentos
                if (attempts >= 3)
                {
                    var blockDateTime = DateTime.Now.AddSeconds(30);
                    HttpContext.Session.SetString("BlockTime", blockDateTime.ToString());

                    // Enviar estado de bloqueo a la vista
                    TempData["IsBlocked"] = true;
                    TempData["BlockSeconds"] = 30;
                    TempData["ErrorMessage"] = "Demasiados intentos fallidos. Cuenta bloqueada por 30 segundos.";

                    // Limpiar intentos restantes durante el bloqueo
                    TempData["RemainingAttempts"] = null;
                }
                else
                {
                    // Calcular y mostrar intentos restantes solo si no hay bloqueo
                    int remainingAttempts = 3 - attempts;
                    TempData["RemainingAttempts"] = remainingAttempts;

                    TempData["ErrorMessage"] = error == "USER_NOT_FOUND"
                        ? $"Usuario no encontrado. Intentos restantes: {remainingAttempts}"
                        : $"Contraseña incorrecta. Intentos restantes: {remainingAttempts}";
                }

                // Guardar preferencia de recordar contraseña
                if (rememberPassword)
                {
                    TempData["RememberPassword"] = true;
                    TempData["RememberEmail"] = email;
                }

                return View();
            }

            // Resetear contadores si el login es exitoso
            HttpContext.Session.Remove("LoginAttempts");
            HttpContext.Session.Remove("BlockTime");

            // Guardar preferencia de recordar contraseña
            if (rememberPassword)
            {
                TempData["RememberPassword"] = true;
            }

            // Resto del código de autenticación...
            var claims = new List<Claim>
             {
                     new Claim(ClaimTypes.Name, usuario.Nombre),
                     new Claim(ClaimTypes.Email, usuario.Email),
                     new Claim(ClaimTypes.Role, usuario.Rol)
             };          

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}