using Prueba21.Models;

namespace Prueba21.Service.Interfaces
{
    public interface IFormaDePagoService
    {
        Task<List<FormaDePago>> GetAllAsync();
        Task<FormaDePago?> GetByIdAsync(int id);
        Task<bool> CreateAsync(FormaDePago formaDePago);
        Task<bool> UpdateAsync(int id, FormaDePago formaDePago);
        Task<bool> DeleteAsync(int id);
    }
}
