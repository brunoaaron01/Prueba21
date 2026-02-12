using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IPersonalService
    {
        Task<List<Personal>> ObtenerTodo();
        Task<Personal> ObtenerPorId(int id);
        Task<bool> CrearPersonal(Personal personal);
        Task<bool> EditarPersonal(Personal personal);
        Task<bool> EliminarPersonal(int id);
    }
}
