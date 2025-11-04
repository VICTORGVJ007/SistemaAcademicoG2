using SistemaAcademicoG2.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IGradoInscripcionRepository
    {
        Task<IEnumerable<GradoInscripcion>> GetAllAsync();
        Task<GradoInscripcion?> GetByIdAsync(int id);
        Task<IEnumerable<GradoInscripcion>> GetByGradoIdAsync(int idGrado);
        Task<IEnumerable<GradoInscripcion>> GetByInscripcionIdAsync(int idInscripcion);
        Task<IEnumerable<GradoInscripcion>> GetByEstadoAsync(int estado);

        Task AddAsync(GradoInscripcion entity);
        Task UpdateAsync(GradoInscripcion entity);
        Task DeleteAsync(int id);

        Task<bool> GradoInscripcionExistsAsync(int id);
    }
}
