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
        // Obtener todas las asignaturas
        Task<IEnumerable<Asignatura>> GetAsignaturasAsync();

        // Obtener una asignatura por su id
        Task<Asignatura> GetAsignaturaByIdAsync(int id);

        // Agregar una nueva asignatura
        Task<Asignatura> AddAsignaturaAsync(Asignatura asignatura);

        // Actualizar una asignatura existente
        Task<Asignatura> UpdateAsignaturaAsync(Asignatura asignatura);

        // Eliminar una asignatura por su id
        Task<bool> DeleteAsignaturaAsync(int id);
    }
}

