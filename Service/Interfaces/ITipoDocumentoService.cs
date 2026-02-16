using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface ITipoDocumentoService
    {
        Task<List<TipoDocumento>> ObtenerTodo();
        Task<TipoDocumento> ObtenerPorId(int id);
    }
}