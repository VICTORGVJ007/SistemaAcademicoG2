using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IRolRepository
    {
        // Operaciones CRUD básicas
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol> GetByIdAsync(int id);
        Task AddAsync(Rol rol); 
        Task UpdateAsync(Rol rol);
        Task DeleteAsync(int id);

        // Métodos adicionales útiles
        Task<IEnumerable<Rol>> GetByEstadoAsync(string estado);
        Task<bool> RolExistsAsync(int id);
        Task<bool> RolExistsAsync(string nombre);
    }
}
