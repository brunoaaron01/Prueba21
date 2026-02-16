using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

namespace Prueba21.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PersonalController : Controller
    {
        private readonly IPersonalService _personalService;
        private readonly IRolService _rolService;
        private readonly ITipoDocumentoService _tipoDocumentoService;

        public PersonalController(
            IPersonalService personalService,
            IRolService rolService,
            ITipoDocumentoService tipoDocumentoService)
        {
            _personalService = personalService;
            _rolService = rolService;
            _tipoDocumentoService = tipoDocumentoService;
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
        public async Task<IActionResult> Create()
        {
            await CargarListasDesplegables();
            return View();
        }

        // POST: Personal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Nombre,TipoDocumento,NumDocumento,Email,IdRol,Activo")] Personal personal,
            string Contrasenia)
        {
            if (!ModelState.IsValid)
            {
                await CargarListasDesplegables();
                return View(personal);
            }

            if (string.IsNullOrWhiteSpace(Contrasenia))
            {
                ModelState.AddModelError("Contrasenia", "La contraseña es obligatoria.");
                await CargarListasDesplegables();
                return View(personal);
            }

            bool resultado = await _personalService.CrearPersonal(personal, Contrasenia);

            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al crear el personal. Verifique que el documento no esté duplicado.");
                await CargarListasDesplegables();
                return View(personal);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Personal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var personal = await _personalService.ObtenerPorId(id.Value);

            if (personal == null) return NotFound();

            await CargarListasDesplegables();
            return View(personal);
        }

        // POST: Personal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("IdPersonal,Nombre,TipoDocumento,NumDocumento,Email,IdRol,Activo")] Personal personal,
            string? Contrasenia)
        {
            if (id != personal.IdPersonal) return NotFound();

            if (!ModelState.IsValid)
            {
                await CargarListasDesplegables();
                return View(personal);
            }

            bool resultado = await _personalService.EditarPersonal(personal, Contrasenia);

            if (!resultado)
            {
                ModelState.AddModelError("", "❌ Error al editar el personal.");
                await CargarListasDesplegables();
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

        // Método auxiliar para cargar listas desplegables
        private async Task CargarListasDesplegables()
        {
            var roles = await _rolService.ObtenerTodo();
            var tiposDocumento = await _tipoDocumentoService.ObtenerTodo();

            ViewBag.Roles = new SelectList(roles, "IdRol", "Nombre");
            ViewBag.TiposDocumento = new SelectList(tiposDocumento, "IdTipoDocumento", "Descripcion");
        }
    }
}