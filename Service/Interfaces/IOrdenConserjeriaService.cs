using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IOrdenConserjeriaService
    {
        Task<List<OrdenConserjeria>> ObtenerTodas();
        Task<OrdenConserjeria> ObtenerPorId(int id);
        Task<bool> CrearOrden(OrdenConserjeria orden);
        Task<bool> FinalizarOrden(int id);
        Task<bool> EliminarOrden(int id);
        Task<Dictionary<string, SelectList>> ObtenerListasDeSeleccionAsync(int? habitacionId = null, int? personalId = null);
    }
}