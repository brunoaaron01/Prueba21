using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba21.Models;
using Prueba21.Service.Interfaces;

[Authorize(Roles = "Administrador")]
public class FormaDePagoController : Controller
{
    private readonly IFormaDePagoService _service;

    public FormaDePagoController(IFormaDePagoService service)
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

        var formaDePago = await _service.GetByIdAsync(id.Value);
        if (formaDePago == null) return NotFound();

        return View(formaDePago);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("FormaDePagoId,Nombre")] FormaDePago formaDePago)
    {
        if (!ModelState.IsValid) return View(formaDePago);

        var created = await _service.CreateAsync(formaDePago);
        if (!created) return BadRequest("Error al crear la forma de pago.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var formaDePago = await _service.GetByIdAsync(id.Value);
        if (formaDePago == null) return NotFound();

        return View(formaDePago);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("FormaDePagoId,Nombre")] FormaDePago formaDePago)
    {
        if (!ModelState.IsValid) return View(formaDePago);

        var updated = await _service.UpdateAsync(id, formaDePago);
        if (!updated) return BadRequest("Error al actualizar la forma de pago.");

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var formaDePago = await _service.GetByIdAsync(id.Value);
        if (formaDePago == null) return NotFound();

        return View(formaDePago);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return BadRequest("Error al eliminar la forma de pago.");

        return RedirectToAction(nameof(Index));
    }
}