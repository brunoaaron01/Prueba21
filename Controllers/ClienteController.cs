using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            return View(await _clienteService.GetClientesAsync());
        }

        // GET: Cliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest("El ID del cliente es obligatorio.");

            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id.Value);
                return View(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: Cliente/Create
        public IActionResult Create(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nombre,Email,Telefono")] Cliente cliente, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _clienteService.AddClienteAsync(cliente);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(cliente);
            }
        }

        // GET: Cliente/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest("El ID del cliente es obligatorio.");

            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id.Value);
                return View(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Email,Telefono")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return BadRequest("El ID del cliente no coincide.");

            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                await _clienteService.UpdateClienteAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(cliente);
            }
        }

        // GET: Cliente/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest("El ID del cliente es obligatorio.");

            try
            {
                var cliente = await _clienteService.GetClienteByIdAsync(id.Value);
                return View(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _clienteService.DeleteClienteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
