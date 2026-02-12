using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class OrdenReservaController : Controller
    {
        private readonly IOrdenReservaService _ordenReservaService;

        public OrdenReservaController(IOrdenReservaService ordenReservaService)
        {
            _ordenReservaService = ordenReservaService;
        }

        
        public async Task<IActionResult> Index()
        {
            var ordenes = await _ordenReservaService.ObtenerTodas();
            return View(ordenes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var ordenReserva = await _ordenReservaService.ObtenerPorId(id.Value);
            return ordenReserva == null ? NotFound() : View(ordenReserva);
        }


        public async Task<IActionResult> Create()
        {
            ViewData["ClienteId"] = await _ordenReservaService.ObtenerClientesSelectList();
            ViewData["FormaDePagoId"] = await _ordenReservaService.ObtenerFormasDePagoSelectList();
            ViewData["HabitacionId"] = await _ordenReservaService.ObtenerHabitacionesDisponiblesSelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,HabitacionId,FormaDePagoId,FechaEntrada,FechaSalida")] OrdenReserva ordenReserva)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = await _ordenReservaService.ObtenerClientesSelectList(ordenReserva.ClienteId);
                ViewData["FormaDePagoId"] = await _ordenReservaService.ObtenerFormasDePagoSelectList(ordenReserva.FormaDePagoId);
                ViewData["HabitacionId"] = await _ordenReservaService.ObtenerHabitacionesDisponiblesSelectList(ordenReserva.HabitacionId);
                return View(ordenReserva);
            }

            bool resultado = await _ordenReservaService.CrearOrden(ordenReserva);
            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al crear la orden.");
                ViewData["ClienteId"] = await _ordenReservaService.ObtenerClientesSelectList(ordenReserva.ClienteId);
                ViewData["FormaDePagoId"] = await _ordenReservaService.ObtenerFormasDePagoSelectList(ordenReserva.FormaDePagoId);
                ViewData["HabitacionId"] = await _ordenReservaService.ObtenerHabitacionesDisponiblesSelectList(ordenReserva.HabitacionId);
                return View(ordenReserva);
            }

            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ordenReserva = await _ordenReservaService.ObtenerPorId(id.Value);
            return ordenReserva == null ? NotFound() : View(ordenReserva);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool resultado = await _ordenReservaService.EliminarOrden(id);
            return resultado ? RedirectToAction(nameof(Index)) : NotFound();
        }
    }
}
