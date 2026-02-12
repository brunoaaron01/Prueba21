using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador,Recepcionista")]
    public class OrdenHospedajeController : Controller
    {
        private readonly IOrdenHospedajeService _service;

        public OrdenHospedajeController(IOrdenHospedajeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var ordenes = await _service.ObtenerOrdenesHospedajeAsync();
            return View(ordenes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ordenHospedaje = await _service.ObtenerOrdenHospedajePorIdAsync(id);
            if (ordenHospedaje == null)
            {
                return NotFound();
            }
            return View(ordenHospedaje);
        }

        public async Task<IActionResult> Create()
        {
            var listasDeSeleccion = await _service.ObtenerListasDeSeleccionAsync();
            foreach (var item in listasDeSeleccion)
            {
                ViewData[item.Key] = item.Value;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdenHospedaje ordenHospedaje)
        {
            if (!ModelState.IsValid)
            {
                var listasDeSeleccion = await _service.ObtenerListasDeSeleccionAsync(ordenHospedaje.ClienteId, ordenHospedaje.FormaDePagoId, ordenHospedaje.HabitacionId, ordenHospedaje.OrdenReservaId);
                foreach (var item in listasDeSeleccion)
                {
                    ViewData[item.Key] = item.Value;
                }
                return View(ordenHospedaje);
            }

            if (!await _service.CrearOrdenHospedajeAsync(ordenHospedaje))
            {
                ModelState.AddModelError("", "❌ No se pudo crear la orden.");
                return View(ordenHospedaje);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarHospedaje(int id)
        {
            await _service.FinalizarHospedajeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("NO existe valor en el ID");
            }

            try
            {
                var ordenHospedaje = await _service.ObtenerOrdenHospedajePorIdAsync(id.Value);
                return View(ordenHospedaje);
            } catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.EliminarOrdenHospedajeAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
