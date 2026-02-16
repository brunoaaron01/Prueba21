using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IPersonalService
    {
        Task<List<Personal>> ObtenerTodo();
        Task<Personal> ObtenerPorId(int id);
        Task<bool> CrearPersonal(Personal personal, string contrasenia);
        Task<bool> EditarPersonal(Personal personal, string? contrasenia = null);
        Task<bool> EliminarPersonal(int id);
    }
}