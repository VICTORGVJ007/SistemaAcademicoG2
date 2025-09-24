using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IAsistenciaRepository
{
    Task<IEnumerable<Asistencia>> GetAllAsync();
    Task<Asistencia> GetByIdAsync(int id);
    Task AddAsync(Asistencia asistencia);
    Task UpdateAsync(Asistencia asistencia);
    Task DeleteAsync(int id); 

    // Extras útiles
    Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha);
    Task<IEnumerable<Asistencia>> GetByNombreAsync(string nombre);
    Task<bool> AsistenciaExistsAsync(int id);
}

