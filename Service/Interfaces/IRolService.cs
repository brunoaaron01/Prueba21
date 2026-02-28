using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IRolService
    {
        Task<List<Rol>> ObtenerTodo();
        Task<Rol> ObtenerPorId(int id);
    }
}