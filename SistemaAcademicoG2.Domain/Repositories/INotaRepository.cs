using SistemaAcademicoG2.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> GetNotasAsync();
        Task<Nota?> GetNotaByIdAsync(int id);
        Task<Nota> AddNotaAsync(Nota nota);
        Task<Nota> UpdateNotaAsync(Nota nota);

        // Nuevo: Soft delete
        Task<bool> DesactivarNotaAsync(int id);

        // Nuevo: Reactivar nota
        Task<bool> ActivarNotaAsync(int id);

        Task<bool> NotaExistsAsync(int id);
    }
}

