using SistemaAcademicoG2.Domain.Entities;

public interface IDocenteAsignaturaGradoRepository
{
    Task<IEnumerable<DocenteAsignaturaGrado>> GetActivosAsync();
    Task<IEnumerable<DocenteAsignaturaGrado>> GetInactivosAsync();
    Task<DocenteAsignaturaGrado?> GetByIdAsync(int id);
    Task AddAsync(DocenteAsignaturaGrado entidad);
    Task<bool> ActivarAsync(int id);
    Task<bool> DesactivarAsync(int id);
    Task<bool> ExisteDuplicadoAsync(int idUsuario, int idGradoAsignatura);
    // Nuevo método: obtener asignaturas filtradas por grado
    Task<List<GradoAsignatura>> GetAsignaturasPorGradoAsync(int idGrado);
    Task SaveChangesAsync();
}
