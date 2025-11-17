using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.DAL.Repositories
{
    public interface IDocenteAsignaturaGradoRepository
    {
        Task<IEnumerable<DocenteAsignaturaGrado>> GetActivosAsync();
        Task<IEnumerable<DocenteAsignaturaGrado>> GetInactivosAsync();
        Task<DocenteAsignaturaGrado?> GetByIdAsync(int id);
        Task<bool> ExisteRelacionAsync(int idUsuario, int idGrado, int idAsignatura);

        Task AddAsync(DocenteAsignaturaGrado entidad);
        Task UpdateAsync(DocenteAsignaturaGrado entidad);

        Task<bool> DesactivarAsync(int id);
        Task<bool> ActivarAsync(int id);
    }
}
