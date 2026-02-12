using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;


[Authorize(Roles = "Administrador,Recepcionista")]
public class HabitacionController : Controller
{
    private readonly IHabitacionService _service;

    public HabitacionController(IHabitacionService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var habitacion = await _service.GetByIdAsync(id.Value);
        if (habitacion == null) return NotFound();

        return View(habitacion);
    }

    [Authorize(Roles = "Administrador")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Create([Bind("HabitacionId,Numero,Tipo,Precio,Estado")] Habitacion habitacion)
    {
        if (!ModelState.IsValid) return View(habitacion);

        var created = await _service.CreateAsync(habitacion);
        if (!created) return BadRequest("Error al crear la habitación.");

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var habitacion = await _service.GetByIdAsync(id.Value);
        if (habitacion == null) return NotFound();

        return View(habitacion);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Edit(int id, [Bind("HabitacionId,Numero,Tipo,Precio,Estado")] Habitacion habitacion)
    {
        if (!ModelState.IsValid) return View(habitacion);

        var updated = await _service.UpdateAsync(id, habitacion);
        if (!updated) return BadRequest("Error al actualizar la habitación.");

        return RedirectToAction(nameof(Index));
    }


    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var habitacion = await _service.GetByIdAsync(id.Value);
        if (habitacion == null) return NotFound();

        return View(habitacion);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return BadRequest("Error al eliminar la habitación.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> CheckOut(int id)
    {
        var checkedOut = await _service.CheckOutAsync(id);
        if (!checkedOut) return BadRequest("Error al realizar el check-out.");

        return RedirectToAction(nameof(Index));
    }
}
