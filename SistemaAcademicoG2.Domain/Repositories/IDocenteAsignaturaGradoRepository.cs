using SistemaAcademicoG2.Domain.Entities;

public interface IDocenteAsignaturaGradoRepository
{
    Task<IEnumerable<DocenteAsignaturaGrado>> GetActivosAsync();
    Task<IEnumerable<DocenteAsignaturaGrado>> GetInactivosAsync();
    Task<DocenteAsignaturaGrado?> GetByIdAsync(int id);
    Task AddAsync(DocenteAsignaturaGrado entidad);
    Task<bool> ActivarAsync(int id);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ExisteDuplicadoAsync(int idUsuario, int idGrado, int idAsignatura);
    Task SaveChangesAsync();
}
