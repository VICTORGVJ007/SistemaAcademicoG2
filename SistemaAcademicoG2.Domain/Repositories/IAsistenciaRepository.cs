using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAsistenciaRepository
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<IEnumerable<Asistencia>> GetActivasAsync();
    Task<IEnumerable<Asistencia>> GetInactivasAsync();

    Task<Asistencia> GetByIdAsync(int id);
    Task AddAsync(Asistencia asistencia);
    Task UpdateAsync(Asistencia asistencia);

    Task<bool> DesactivarAsync(int id);
    Task<bool> ActivarAsync(int id);

    Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha);
    Task<IEnumerable<Asistencia>> GetByUsuarioAsync(int idUsuario);

    Task<bool> AsistenciaExistsAsync(int id);
}
