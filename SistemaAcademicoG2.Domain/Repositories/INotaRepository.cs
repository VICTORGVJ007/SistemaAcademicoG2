using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> GetNotasWithIncludesAsync();
        Task<Nota?> GetNotaByIdWithIncludesAsync(int id);

        Task AddNotaAsync(Nota nota);
        Task UpdateNotaAsync(Nota nota);
        Task<bool> DesactivarNotaAsync(int id);
        Task<bool> NotaExistsAsync(int id);
    }
}
