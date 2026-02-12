using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IOrdenReservaService
    {
        Task<List<OrdenReserva>> ObtenerTodas();
        Task<OrdenReserva> ObtenerPorId(int id);
        Task<bool> CrearOrden(OrdenReserva ordenReserva);
        Task<bool> EliminarOrden(int id);
        Task<SelectList> ObtenerClientesSelectList(int? seleccionado = null);
        Task<SelectList> ObtenerFormasDePagoSelectList(int? seleccionado = null);
        Task<SelectList> ObtenerHabitacionesDisponiblesSelectList(int? seleccionado = null);
    }
}
