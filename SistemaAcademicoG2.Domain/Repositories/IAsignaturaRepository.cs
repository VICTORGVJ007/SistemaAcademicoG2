using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    // Contrato del repositorio de asignaturas
    public interface IAsignaturaRepository
    {
        Task<IEnumerable<Asignatura>> GetAsignaturasAsync();
        Task<IEnumerable<Asignatura>> GetAsignaturasActivasAsync();
        Task<IEnumerable<Asignatura>> GetAsignaturasInactivasAsync();

        Task<Asignatura?> GetAsignaturaByIdAsync(int id);

        Task<Asignatura> AddAsignaturaAsync(Asignatura asignatura);
        Task<Asignatura> UpdateAsignaturaAsync(Asignatura asignatura);

        Task<bool> DesactivarAsignaturaAsync(int id);
        Task<bool> ActivarAsignaturaAsync(int id);

        Task<bool> AsignaturaExistsAsync(int id);

        // NUEVO
        Task<bool> NombreExisteAsync(string nombre);
    }
}

