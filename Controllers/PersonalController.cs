using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PersonalController : Controller
    {
        private readonly IPersonalService _personalService;

        public PersonalController(IPersonalService personalService)
        {
            _personalService = personalService;
        }

        // GET: Personal
        public async Task<IActionResult> Index()
        {
            var personalList = await _personalService.ObtenerTodo();
            return View(personalList);
        }

        // GET: Personal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var personal = await _personalService.ObtenerPorId(id.Value);
            return personal == null ? NotFound() : View(personal);
        }

        // GET: Personal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonalId,Nombre,Email,Contrasenia,Rol")] Personal personal)
        {
            if (!ModelState.IsValid) return View(personal);

            bool resultado = await _personalService.CrearPersonal(personal);
            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al crear el personal.");
                return View(personal);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Personal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var personal = await _personalService.ObtenerPorId(id.Value);
            return personal == null ? NotFound() : View(personal);
        }

        // POST: Personal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonalId,Nombre,Email,Contrasenia,Rol")] Personal personal)
        {
            if (id != personal.PersonalId) return NotFound();
            if (!ModelState.IsValid) return View(personal);

            bool resultado = await _personalService.EditarPersonal(personal);
            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al editar el personal.");
                return View(personal);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Personal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var personal = await _personalService.ObtenerPorId(id.Value);
            return personal == null ? NotFound() : View(personal);
        }

        // POST: Personal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            bool resultado = await _personalService.EliminarPersonal(id);
            return resultado ? RedirectToAction(nameof(Index)) : NotFound();
        }
    }
}