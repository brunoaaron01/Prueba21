using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IHabitacionService
    {
        Task<List<Habitacion>> GetAllAsync();
        Task<Habitacion?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Habitacion habitacion);
        Task<bool> UpdateAsync(int id, Habitacion habitacion);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckOutAsync(int id);
    }
}
