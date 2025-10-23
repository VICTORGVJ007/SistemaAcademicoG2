using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAsistenciaRepository
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<Asistencia> GetByIdAsync(int id);
    Task AddAsync(Asistencia asistencia);
    Task UpdateAsync(Asistencia asistencia);
    Task DeleteAsync(int id);
    Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha);

    // ✅ Buscar asistencias por ID de usuario (entero)
    Task<IEnumerable<Asistencia>> GetByUsuarioAsync(int idUsuario);

    Task<bool> AsistenciaExistsAsync(int id);
}
