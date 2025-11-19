using SistemaAcademicoG2.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGradoAsignaturaRepository
{
    // Obtener todas las asignaciones
    Task<IEnumerable<GradoAsignatura>> GetAllAsync();

    // Obtener asignaciones activas e inactivas
    Task<IEnumerable<GradoAsignatura>> GetActivasAsync();
    Task<IEnumerable<GradoAsignatura>> GetInactivasAsync();

    // Obtener por Id
    Task<GradoAsignatura?> GetByIdAsync(int id);

    // Agregar y actualizar
    Task AddAsync(GradoAsignatura entidad);
    Task UpdateAsync(GradoAsignatura entidad);

    // Activar y desactivar
    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);

    // Verificar existencia
    Task<bool> GradoAsignaturaExistsAsync(int id);
}
