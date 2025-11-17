using SistemaAcademicoG2.Domain.Entities;

public interface IPeriodoRepository
{
    Task<IEnumerable<Periodo>> GetActivosAsync();
    Task<IEnumerable<Periodo>> GetInactivosAsync();
    Task<Periodo> GetByIdAsync(int id);
    Task AddAsync(Periodo periodo);
    Task UpdateAsync(Periodo periodo);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);
}

