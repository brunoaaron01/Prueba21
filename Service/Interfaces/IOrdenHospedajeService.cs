using Microsoft.AspNetCore.Mvc.Rendering;
using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IOrdenHospedajeService
    {
        Task<List<OrdenHospedaje>> ObtenerOrdenesHospedajeAsync();
        Task<Dictionary<string, SelectList>> ObtenerListasDeSeleccionAsync(int? clienteId = null, int? formaDePagoId = null, int? habitacionId = null, int? ordenReservaId = null);
        Task<OrdenHospedaje?> ObtenerOrdenHospedajePorIdAsync(int id);
        Task<bool> CrearOrdenHospedajeAsync(OrdenHospedaje ordenHospedaje);
        Task<bool> FinalizarHospedajeAsync(int id);
        Task<bool> EliminarOrdenHospedajeAsync(int id);
    }
}
