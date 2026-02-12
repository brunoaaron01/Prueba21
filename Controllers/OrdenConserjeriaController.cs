using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador,Conserje")]
    public class OrdenConserjeriaController : Controller
    {
        private readonly IOrdenConserjeriaService _ordenConserjeriaService;

        public OrdenConserjeriaController(IOrdenConserjeriaService ordenConserjeriaService)
        {
            _ordenConserjeriaService = ordenConserjeriaService;
        }

        
        public async Task<IActionResult> Index()
        {
            var ordenes = await _ordenConserjeriaService.ObtenerTodas();
            return View(ordenes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var orden = await _ordenConserjeriaService.ObtenerPorId(id.Value);
            return orden == null ? NotFound() : View(orden);
        }

        public async Task<IActionResult> Create()
        {
            var listasDeSeleccion = await _ordenConserjeriaService.ObtenerListasDeSeleccionAsync();
            foreach (var item in listasDeSeleccion)
            {
                ViewData[item.Key] = item.Value;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrdenConserjeria orden)
        {
            if (!ModelState.IsValid)
            {
                var listasDeSeleccion = await _ordenConserjeriaService.ObtenerListasDeSeleccionAsync(orden.HabitacionId, orden.PersonalId);
                foreach (var item in listasDeSeleccion)
                {
                    ViewData[item.Key] = item.Value;
                }
                return View(orden);
            }

            bool resultado = await _ordenConserjeriaService.CrearOrden(orden);
            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al crear la orden.");
                var listasDeSeleccion = await _ordenConserjeriaService.ObtenerListasDeSeleccionAsync(orden.HabitacionId, orden.PersonalId);
                foreach (var item in listasDeSeleccion)
                {
                    ViewData[item.Key] = item.Value;
                }
                return View(orden);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> FinalizarOrdenConserjeria(int id)
        {
            bool resultado = await _ordenConserjeriaService.FinalizarOrden(id);
            return resultado ? RedirectToAction(nameof(Index)) : NotFound();
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var orden = await _ordenConserjeriaService.ObtenerPorId(id.Value);
            return orden == null ? NotFound() : View(orden);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool resultado = await _ordenConserjeriaService.EliminarOrden(id);
            return resultado ? RedirectToAction(nameof(Index)) : NotFound();
        }
    }
}